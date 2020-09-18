using KNU.PR.NewsManager.Models.NewsModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.PR.NewsManager.Servcies.ApiHandler
{
    public interface IApiHandler
    {
        Task<List<Article>> GetLast24HoursNewsAsync();
    }
}