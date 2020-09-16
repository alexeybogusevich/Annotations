using KNU.PR.NewsSaver.Models.NewsItemTag;
using System.Collections.Generic;

namespace KNU.PR.NewsSaver.Servcies.TagService
{
    public interface ITagService
    {
        List<NewsItemTag> GetTopTagsForNewsItem(string text);
    }
}