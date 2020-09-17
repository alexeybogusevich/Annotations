using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.DbManager.Models
{
    public class TagClusterEntity
    {
        public Guid Id { get; set; }
        public Guid ClusterId { get; set; }
        public ClusterEntity Cluster { get; set; }
        public Guid TagId { get; set; }
        [JsonIgnore]
        public TagEntity Tag { get; set; }
        public int OccurencesCount { get; set; }
        public double NormOccurencesCount { get; set; }
    }
}
