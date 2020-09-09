using KNU.PR.DbManager.Models;
using KNU.PR.NewsSaver.Models.NewsItemTag;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.PR.NewsSaver.Servcies.DbSaver
{
    public interface IDbSaver
    {
        Task SaveNewsItemAsync(NewsEntity newsEntity);
        Task SaveTagAsync(TagEntity tag);
        Task SaveTagsAndModelAsync(List<NewsItemTag> tags, NewsEntity newsEntity);

    }
}