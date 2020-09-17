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
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    context.TagsNews.Add(
                        new TagClusterEntity
                        {
                            ClusterId = newsEntity.Id,
                            OccurencesCount = tag.OccurencesCount
                        });
                }

                var newTags = new List<TagEntity>();

                var allTags = await context.Tags.Select(t => t.Name).ToListAsync();

                foreach (var tag in tags)
                {
                    if (!allTags.Contains(tag.Name))
                    {
                        newTags.Add(new TagEntity()
                        {
                            Name = tag.Name
                        });
                    }
                }
                context.Tags.AddRange(newTags);
            }

            context.NewsEntities.Add(newsEntity);
            await context.SaveChangesAsync();
        }
    }
}
