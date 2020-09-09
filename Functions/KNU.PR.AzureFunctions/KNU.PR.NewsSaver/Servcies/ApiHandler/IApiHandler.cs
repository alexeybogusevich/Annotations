using KNU.PR.NewsSaver.Models.NewsModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.PR.NewsSaver.Servcies.ApiHandler
{
    public interface IApiHandler
    {
        Task<List<Article>> GetLast24HoursNewsAsync();
    }
}