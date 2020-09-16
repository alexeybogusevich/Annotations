using KNU.PR.NewsSaver.Models.NewsItemTag;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsSaver.Servcies.TagService
{
    public class TagService : ITagService
    {
        private readonly int topTagsCount = 5;

        public TagService()
        {

        }

        public List<NewsItemTag> GetAllTagsForNewsItem(string text)
        {
            return null;
        }
    }
}
