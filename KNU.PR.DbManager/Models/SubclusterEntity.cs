using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.DbManager.Models
{
    public class SubclusterEntity
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public ClusterEntity Parent { get; set; }
        public Guid ChildId { get; set; }
        public ClusterEntity Child { get; set; }
    }
}
