using KNU.PR.DbManager.Connections;
using KNU.PR.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.PR.Annotations.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly AzureSqlDbContext context;

        public NewsService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<NewsEntity> GetNewsEntityAsync(Guid id)
        {
            return await context.NewsEntities.FirstOrDefaultAsync(n => n.Id.Equals(id));
        }

        public async Task<List<NewsEntity>> GetAllNewsAsync()
        {
            return await context.NewsEntities.ToListAsync();
        }

        public async Task<List<NewsEntity>> GetNewsByTagAsync(Guid tagId)
        {
            // var tagNewsIds = await context.TagsNews.Where(t => t.TagId.Equals(tagId)).OrderByDescending(n => n.OccurencesCount).Select(t => t.NewsEntityId).ToListAsync();
            //var news = await context.NewsEntities.Where(n => tagNewsIds.Contains(n.Id)).ToListAsync(); 
            var tagNewsIds = await context.TagsNews.ToListAsync();
            var news = await context.NewsEntities.ToListAsync();
            return news;
        }
    }
}
