using KNU.PR.NewsManager.Models.NewsModel;
using KNU.PR.NewsManager.Models.NewsItemTag;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsManager.Models.Cluster
{
    public class Cluster
    {
        public Guid id;
        public int newsCount;
        public List<Article> articles;
        public List<Tag> tagsVector;

        public Cluster (Guid id, int newsCount, List<Article> articles, List<Tag> tagsVector)
        {
            this.id = id;
            this.newsCount = newsCount;
            this.articles = articles;
            this.tagsVector = tagsVector;
        }
    }
}
