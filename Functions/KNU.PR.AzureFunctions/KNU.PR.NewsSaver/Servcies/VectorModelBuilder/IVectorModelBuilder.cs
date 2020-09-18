using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.VectorModelBuilder
{
    public interface IVectorModelBuilder
    {
        void Process(Dictionary<Guid, List<Tuple<string, double>>> clustersTags);
    }
}
