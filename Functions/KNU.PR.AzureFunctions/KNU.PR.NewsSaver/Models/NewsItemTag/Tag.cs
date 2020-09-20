using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.PR.NewsManager.Models.NewsItemTag
{
    public class Tag
    {
        public string Name { get; set; }
        public int OccurencesCount { get; set; }
        public double NormCount { get; set; }

        public Tag(string name, int count, double normCount)
        {
            this.Name = name;
            this.OccurencesCount = count;
            this.NormCount = normCount;
        }
    }
}
