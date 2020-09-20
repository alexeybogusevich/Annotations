using KNU.PR.NewsManager.Models.Cluster;
using KNU.PR.NewsManager.Servcies.DbGetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.VectorModelBuilder
{
    public class VectorModelBuilder : IVectorModelBuilder
    {
        private readonly double maxCosValue = 0.95;
        private readonly int tagsCount = 10;
        private readonly IDbGetter dbGetter;

        public VectorModelBuilder(IDbGetter dbGetter)
        {
            this.dbGetter = dbGetter;
        }
        public void Process()
        {
            //double[,] matrix = new double[clustersTags.Count, clustersTags.Count] { { 0, 0 } };
        }

        private double similarity(Cluster cluster1, Cluster cluster2)
        {
            double result = 0;

            /*
            foreach ((string, double) tag in vector1)
            {
                if (vector2.Exists(t => t.Item1 == tag.Item1))
                {
                    vector2.Find(t => t.Item1 == tag.Item1).
                }
            }
            */

            return result;
        }
        private double cosineMeasure(List<(string, double)> vector1, List<(string, double)> vector2)
        {
            double result = 0;
            var commonTags = vector1.Select(t => t.Item1).Intersect(vector2.Select(t => t.Item1)).ToList();
            foreach (var tag in commonTags)
            {
                result += vector1.Where(t => t.Item1 == tag).Select(t => t.Item2).FirstOrDefault() *
                    vector2.Where(t => t.Item1 == tag).Select(t => t.Item2).FirstOrDefault();
            }

            result /= (tagsCount * tagsCount);
            return result;
        }
    }
}
