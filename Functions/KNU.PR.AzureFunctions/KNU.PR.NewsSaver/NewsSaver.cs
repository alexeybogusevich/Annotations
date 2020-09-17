using KNU.PR.NewsSaver.Servcies.ApiHandler;
using KNU.PR.NewsSaver.Servcies.DbSaver;
using KNU.PR.NewsSaver.Servcies.EntityConverter;
using KNU.PR.NewsSaver.Servcies.Filter;
using KNU.PR.NewsSaver.Servcies.TagService;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNU.PR.NewsSaver
{
    public class NewsSaver
    {
        private readonly IApiHandler apiHandler;
        private readonly IDbSaver dbSaver;
        private readonly ITagService tagService;
        private readonly IEntityConverter entityConverter;
        private readonly IFilter stopWordsFilter;
        private readonly IFilter porterFilter;

        public NewsSaver(IApiHandler apiHandler, IDbSaver dbSaver, ITagService tagService, IEntityConverter entityConverter)
        {
            this.apiHandler = apiHandler;
            this.dbSaver = dbSaver;
            this.tagService = tagService;
            this.entityConverter = entityConverter;
            this.stopWordsFilter = new StopWordsFilter();
            this.porterFilter = new PorterStemmerFilter();
        }

        [FunctionName(nameof(NewsSaver))]
        public async Task RunAsync([TimerTrigger("00 10 * * *", RunOnStartup = true)] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            if (timer.IsPastDue)
            {
                log.LogInformation("Job is running late.");
            }
            var lastDayNews = await apiHandler.GetLast24HoursNewsAsync();
            log.LogInformation($"C# Timer trigger function proccessed last day news at: {DateTime.Now}. Count: {lastDayNews.Count}");

            /* DEBUG */

            var text = "Slideshow(2 images)\n\nBARCELONA(Reuters) - Lionel Messi scored twice as Barcelona eased to a 3 - 1 victory over Girona in their second pre - season friendly under Ronald Koeman on Wednesday.\n\nPhilippe Coutinho struck the crossbar early on before giving Barca the lead midway through the first half, tapping into an empty net after brilliant link play between Messi and new signing Francisco Trincao.\n\nMessi then doubled Barca’s advantage later in the half, spinning away from a defender to thump a shot in off the post and score for the first time since threatening to leave the club last month.\n\nSamu Saiz scored for Girona right at the start of the second period but Barca’s captain struck again in the 50th minute, also cutting inside to shoot from outside the area but this time benefiting from a heavy deflection.\n\nKoeman made four changes to the lineup that had beaten Gimnastic de Tarragona 3 - 1 in his first game on Saturday and made nine substitutions midway through the second half, only keeping on defender Ronald Araujo and goalkeeper Neto.\n\nThis was a more fluid and exciting display than against Gimnastic, with Messi looking sharper and Portuguese Trincao showing confidence and flair every time he got on the ball.\n\nTeenager Pedri was Barca’s liveliest substitute and had three attempts on goal after coming on, forcing Girona keeper Jose Suarez to make two smart saves, while American forward Konrad de la Fuente also impressed.\n\nBarca play promoted La Liga side Elche in their final pre - season game on Saturday before beginning their league campaign the following weekend at home to Villarreal.";
            var filtered = porterFilter.Process(stopWordsFilter.Process(text));
            var tagsList = tagService.GetTopTagsForNewsItem(filtered);

            /* END DEBUG */

            foreach (var item in lastDayNews)
            {
                var entity = entityConverter.ConvertArticle(item);

                // Removing stop words and normalizing the words
                var filteredItem = porterFilter.Process(stopWordsFilter.Process(item.Content));

                // Get top 5 tags from article
                var tags = tagService.GetTopTagsForNewsItem(filteredItem);

                // Insert article to DB, update current tags with new ones
                await dbSaver.SaveTagsAndModelAsync(tags, entity);
                log.LogInformation($"Article saved. Url: {item.Url}.");
            }
            log.LogInformation($"C# Timer trigger function finished execution at: {DateTime.Now}");
        }
    }
}
