using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsSaver.Servcies.Filter
{
    public interface IFilter
    {
        string Process(string text);
    }
}
