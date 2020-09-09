using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.DbManager.Models
{
    public class TagNewsEntity
    {
        public Guid Id { get; set; }
        public Guid NewsEntityId { get; set; }
        [JsonIgnore]
        public NewsEntity NewsEntity { get; set; }
        public Guid TagId { get; set; }
        [JsonIgnore]
        public TagEntity Tag { get; set; }
        public int OccurencesCount { get; set; }
    }
}
