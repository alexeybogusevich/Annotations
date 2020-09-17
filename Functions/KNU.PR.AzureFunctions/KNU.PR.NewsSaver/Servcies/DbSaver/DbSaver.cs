using KNU.PR.DbManager.Connections;
using KNU.PR.DbManager.Models;
using KNU.PR.NewsSaver.Models.NewsItemTag;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNU.PR.NewsSaver.Servcies.DbSaver
{
    public class DbSaver : IDbSaver
    {
        private readonly AzureSqlDbContext context;

        public DbSaver(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task SaveNewsItemAsync(NewsEntity newsEntity)
        {
            context.NewsEntities.Add(newsEntity);
            await context.SaveChangesAsync();
        }

        public async Task SaveTagAsync(TagEntity tag)
        {
            context.Tags.Add(tag);
            await context.SaveChangesAsync();
        }

        public async Task SaveTagsAndModelAsync(List<NewsItemTag> tags, NewsEntity newsEntity)
        {
            // Check if this article is already in database
            var allNewsDesctiptions = await context.NewsEntities.Select(t => t.Description).ToListAsync();
            if (allNewsDesctiptions.Contains(newsEntity.Description))
            {
                throw new DbUpdateException("News article already in DB.");
            }

            // Create new cluster for news item
            var cluster = new ClusterEntity { Id = Guid.NewGuid() };
            // Link cluster to news item
            newsEntity.ClusterId = cluster.Id;

            if (tags != null)
            {
                var newTags = new List<TagEntity>();
                var allTags = await context.Tags.Select(t => t).ToListAsync();

                foreach (var tag in tags)
                {
                    if (!allTags.Any(t => t.Name == tag.Name))
                    {
                        newTags.Add(new TagEntity()
                        {
                            Id = Guid.NewGuid(),
                            Name = tag.Name
                        });
                    }
                }
                context.Tags.AddRange(newTags);
                allTags.AddRange(newTags);

                // Preparing to normalize the occurences vector
                double tagCountSquareSum = 0;
                foreach (var tag in tags)
                {
                    tagCountSquareSum += tag.OccurencesCount * tag.OccurencesCount;
                }
                tagCountSquareSum = Math.Sqrt(tagCountSquareSum);

                // Adding links between clusters and tags
                foreach (var tag in tags)
                {
                    context.TagsClusters.Add(
                        new TagClusterEntity
                        {
                            ClusterId = cluster.Id,
                            TagId = allTags.Where(t => t.Name == tag.Name).FirstOrDefault().Id,
                            OccurencesCount = tag.OccurencesCount,
                            NormOccurencesCount = (double)tag.OccurencesCount / tagCountSquareSum
                        });
                }
            }

            context.Clusters.Add(cluster);
            context.NewsEntities.Add(newsEntity);
            await context.SaveChangesAsync();
        }
    }
}
