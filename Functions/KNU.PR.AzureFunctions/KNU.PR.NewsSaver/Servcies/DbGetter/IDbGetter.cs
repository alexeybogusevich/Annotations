using KNU.PR.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.DbGetter
{
    public interface IDbGetter
    {
        Guid MergeClusters(List<ClusterEntity> clusters);
        Dictionary<Guid, List<Tuple<string, double>>> GetAllClustersAndTags();
    }
}
