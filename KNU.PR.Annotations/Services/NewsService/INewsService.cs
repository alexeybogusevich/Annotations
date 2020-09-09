using KNU.PR.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.PR.Annotations.Services.NewsService
{
    public interface INewsService
    {
        Task<NewsEntity> GetNewsEntityAsync(Guid id);
        Task<List<NewsEntity>> GetAllNewsAsync();
        Task<List<NewsEntity>> GetNewsByTagAsync(Guid tagId);
    }
}