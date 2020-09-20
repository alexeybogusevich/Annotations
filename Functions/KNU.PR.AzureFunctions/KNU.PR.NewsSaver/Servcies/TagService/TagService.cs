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

        public List<Tag> GetTopTagsForNewsItem(string text)
        {
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = new List<Tag>();

            foreach (var word in words)
            {
                if (!result.Exists(delegate (Tag x) { return string.Equals(x.Name, word) ? true : false; }))
                {
                    var newTag = new Tag(word, 1, 0);
                    result.Add(newTag);
                }
                else
                {
                    result.Find(x => x.Name == word).OccurencesCount++;
                }
            }

            var sortedTags = result.OrderByDescending(x => x.OccurencesCount).Take(topTagsCount).ToList();
            double tagCountSquareSum = Math.Sqrt(sortedTags.Sum(t => t.OccurencesCount * t.OccurencesCount));
            
            foreach(var tag in sortedTags)
            {
                tag.NormCount = (double)tag.OccurencesCount / tagCountSquareSum;
            }

            return sortedTags;
        }
    }
}
