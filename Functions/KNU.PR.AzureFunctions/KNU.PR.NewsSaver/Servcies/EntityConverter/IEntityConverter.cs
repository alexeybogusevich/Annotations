using KNU.PR.DbManager.Models;
using KNU.PR.NewsSaver.Models.NewsModel;

namespace KNU.PR.NewsSaver.Servcies.EntityConverter
{
    public interface IEntityConverter
    {
        NewsEntity ConvertArticle(Article article);
    }
}