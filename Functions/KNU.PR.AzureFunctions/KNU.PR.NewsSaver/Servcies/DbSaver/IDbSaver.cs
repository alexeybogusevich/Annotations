using KNU.PR.DbManager.Models;
using KNU.PR.NewsManager.Models.NewsItemTag;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.PR.NewsManager.Servcies.DbSaver
{
    public interface IDbSaver
    {
        Task SaveNewsItemAsync(NewsEntity newsEntity);
        Task SaveTagAsync(TagEntity tag);
        Task SaveTagsAndModelAsync(List<NewsItemTag> tags, NewsEntity newsEntity);
        Task SaveClusterAndTagsAsync(ClusterEntity cluster, List<NewsItemTag> tags);

    }
}