using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsSaver.Models.NewsModel
{
    public class ResponseModel
    {
        [JsonProperty("articles")]
        public List<Article> Articles { get; set; }
        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
