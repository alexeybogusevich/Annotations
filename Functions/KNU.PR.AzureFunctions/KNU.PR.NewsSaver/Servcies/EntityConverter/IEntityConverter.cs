using KNU.PR.DbManager.Models;
using KNU.PR.NewsManager.Models.NewsModel;

namespace KNU.PR.NewsManager.Servcies.EntityConverter
{
    public interface IEntityConverter
    {
        NewsEntity ConvertArticle(Article article);
    }
}