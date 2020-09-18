using KNU.PR.NewsManager.Models.NewsItemTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.TagService
{
    public class TagService : ITagService
    {
        private readonly int topTagsCount = 10;

        public TagService()
        {

        }

        public List<NewsItemTag> GetTopTagsForNewsItem(string text)
        {
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = new List<NewsItemTag>();
            
            foreach (var word in words)
            {
                if (!result.Exists(delegate (NewsItemTag x) { return string.Equals(x.Name, word) ? true : false; }))
                {
                    var newTag = new NewsItemTag(word, 1);
                    result.Add(newTag);
                }
                else
                {
                    result.Find(x => x.Name == word).OccurencesCount++;
                }
            }

            var sorted = result.OrderByDescending(x => x.OccurencesCount).Take(topTagsCount);
            return sorted.ToList();
        }
    }
}
