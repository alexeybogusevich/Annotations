using KNU.PR.NewsManager.Models.NewsItemTag;
using System.Collections.Generic;

namespace KNU.PR.NewsManager.Servcies.TagService
{
    public interface ITagService
    {
        List<NewsItemTag> GetTopTagsForNewsItem(string text);
    }
}