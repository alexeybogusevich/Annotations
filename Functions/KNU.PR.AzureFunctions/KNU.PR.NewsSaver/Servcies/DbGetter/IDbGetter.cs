using KNU.PR.DbManager.Models;
using KNU.PR.NewsManager.Models.Cluster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.DbGetter
{
    public interface IDbGetter
    {
        Guid MergeClusters(List<ClusterEntity> clusters);
        List<Cluster> GetAllClustersAndTags();
    }
}
