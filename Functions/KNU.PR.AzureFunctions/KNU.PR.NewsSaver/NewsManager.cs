using KNU.PR.NewsManager.Servcies.ApiHandler;
using KNU.PR.NewsManager.Servcies.DbGetter;
using KNU.PR.NewsManager.Servcies.DbSaver;
using KNU.PR.NewsManager.Servcies.EntityConverter;
using KNU.PR.NewsManager.Servcies.Filter;
using KNU.PR.NewsManager.Servcies.TagService;
using KNU.PR.NewsManager.Servcies.VectorModelBuilder;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNU.PR.NewsManager
{
    public class NewsManager
    {
        private readonly IApiHandler apiHandler;
        private readonly IDbGetter dbGetter;
        private readonly IDbSaver dbSaver;
        private readonly IEntityConverter entityConverter;
        private readonly IFilter stopWordsFilter;
        private readonly IFilter porterFilter;
        private readonly ITagService tagService;
        private readonly IVectorModelBuilder vectorModelBuilder;

        public NewsManager(IApiHandler apiHandler,
            IDbGetter dbGetter,
            IDbSaver dbSaver,
            ITagService tagService,
            IEntityConverter entityConverter,
            IVectorModelBuilder vmBuilder)
        {
            this.apiHandler = apiHandler;
            this.dbGetter = dbGetter;
            this.dbSaver = dbSaver;
            this.tagService = tagService;
            this.entityConverter = entityConverter;
            this.stopWordsFilter = new StopWordsFilter();
            this.porterFilter = new PorterStemmerFilter();
            this.vectorModelBuilder = vmBuilder;
        }

        [FunctionName(nameof(NewsManager))]
        public async Task RunAsync([TimerTrigger("00 10 * * *", RunOnStartup = true)] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            if (timer.IsPastDue)
            {
                log.LogInformation("Job is running late.");
            }
            var lastDayNews = await apiHandler.GetLast24HoursNewsAsync();
            log.LogInformation($"C# Timer trigger function proccessed last day news at: {DateTime.Now}. Count: {lastDayNews.Count}");

            /*
             * Preprocessing
             */

            foreach (var item in lastDayNews)
            {
                var entity = entityConverter.ConvertArticle(item);

                // Removing stop words and normalizing the words
                var filteredItem = porterFilter.Process(stopWordsFilter.Process(item.Content));

                // Get top 10 tags from article
                var tags = tagService.GetTopTagsForNewsItem(filteredItem);

                // Insert article to DB, update current tags with new ones
                try
                {
                    await dbSaver.SaveTagsAndModelAsync(tags, entity);
                    log.LogInformation($"Article saved. Url: {item.Url}.");
                }
                catch (Exception e)
                {
                    log.LogInformation($"Exception while saving the article. Error message: {e.Message}");
                }
            }

            log.LogInformation($"C# Timer trigger function finished execution at: {DateTime.Now}");

            /*
             * Processing
             */

            // Delete all from Subcluster Entity

            // Delete all clusters with NewsCount > 1 (from ClusterEntity, TagClusterEntity)

            // Dump all clusters and their vectors to VectorModelBuilder
            //var clustersTags = dbGetter.GetAllClustersAndTags();
            //vectorModelBuilder.Process(clustersTags);
        }
    }
}
