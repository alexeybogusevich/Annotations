using KNU.PR.DbManager.Models;
using KNU.PR.NewsManager.Models.NewsModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNU.PR.NewsManager.Servcies.EntityConverter
{
    public class EntityConverter : IEntityConverter
    {
        public EntityConverter()
        {

        }

        public NewsEntity ConvertArticle(Article article)
        {
            return new NewsEntity
            {
                SourceName = article.Source.Name,
                Content = article.Content,
                Title = article.Title,
                Description = article.Description,
                Url = article.Url,
                UrlToImage = article.UrlToImage,
                PublishedAt = Convert.ToDateTime(article.PublishedAt),
                AddedAt = DateTime.Now
            };
        }
    }
}
