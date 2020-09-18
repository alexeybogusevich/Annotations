using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.PR.NewsManager.Models.NewsModel
{
    public class Article
    {
        [JsonProperty("source")]
        public Source Source { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("urlToImage")]
        public string UrlToImage { get; set; }
        [JsonProperty("publishedAt")]
        public string PublishedAt { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
