using KNU.PR.DbManager.Connections;
using KNU.PR.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.DbGetter
{
    public class DbGetter : IDbGetter
    {
        private readonly AzureSqlDbContext context;

        public DbGetter(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public Guid MergeClusters(List<ClusterEntity> clusters)
        {
            // Create new cluster
            var newCluster = new ClusterEntity { Id = Guid.NewGuid(), NewsCount = clusters.Sum(c => c.NewsCount) };

            // Add dependencies to Subcluster
            foreach (ClusterEntity cluster in clusters)
            {
                context.Subclusters.Add(new SubclusterEntity { ChildId = cluster.Id, ParentId = newCluster.Id });
            }

            // Add average vector to TagCluster



            context.SaveChangesAsync();

            // Return new Guid
            return newCluster.Id;
        }

        public Dictionary<Guid, List<Tuple<string, double>>> GetAllClustersAndTags()
        {
            var result = new Dictionary<Guid, List<Tuple<string, double>>>();

            var allClusters = context.Clusters.Select(c => c).ToList();
            foreach (ClusterEntity cluster in allClusters)
            {
                var tags = context.TagsClusters
                    .Where(t => t.ClusterId == cluster.Id)
                    .Select(t => new { t.Tag.Name, t.NormOccurencesCount })
                    .AsEnumerable()
                    .Select(t => new Tuple<string, double>(t.Name, t.NormOccurencesCount))
                    .ToList();

                result.Add(cluster.Id, tags);
            }

            return result;
        }
    }
}
