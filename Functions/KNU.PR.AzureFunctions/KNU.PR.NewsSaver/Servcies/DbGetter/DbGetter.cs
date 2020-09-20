using KNU.PR.DbManager.Connections;
using KNU.PR.DbManager.Models;
using KNU.PR.NewsManager.Models.Cluster;
using KNU.PR.NewsManager.Models.NewsItemTag;
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

        public List<Cluster> GetAllClustersAndTags()
        {
            var result = new List<Cluster>();

            var allClusters = context.Clusters.Select(c => c).ToList();
            foreach (ClusterEntity cluster in allClusters)
            {
                var tags = context.TagsClusters
                    .Where(t => t.ClusterId == cluster.Id)
                    .Select(t => new { t.Tag.Name, t.OccurencesCount, t.NormOccurencesCount })
                    .AsEnumerable()
                    .Select(t => new Tag(t.Name, t.OccurencesCount, t.NormOccurencesCount))
                    .ToList();

                /*
                 * TO DO
                 * 
                var articles = context.Subclusters
                    .Where(s=>s.ParentId == cluster.Id)
                var newCluster = new Cluster(cluster.Id, cluster.NewsCount, articles, tags);

                result.Add(newCluster);
                */
            }

            return result;
        }
    }
}
