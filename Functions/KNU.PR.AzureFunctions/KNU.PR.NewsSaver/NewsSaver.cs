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

            foreach (var item in lastDayNews)
            {
                var entity = entityConverter.ConvertArticle(item);
                // Removing stop words and normalizing the words
                //string text = "I eat hamburgers. Lyona eats them too!";
                var filteredItem = porterFilter.Process(stopWordsFilter.Process(item.Content));
                // Update current tags in DB
                
                
                var tags = tagService.GetAllTagsForNewsItem(filteredItem);
                await dbSaver.SaveTagsAndModelAsync(tags, entity);
                log.LogInformation($"Article saved. Url: {item.Url}.");
            }
            log.LogInformation($"C# Timer trigger function finished execution at: {DateTime.Now}");
        }
    }
}
