using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.Filter
{
    public interface IFilter
    {
        string Process(string text);
    }
}
