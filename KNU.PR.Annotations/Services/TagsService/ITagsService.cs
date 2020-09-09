using KNU.PR.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.PR.Annotations.Services.TagsService
{
    public interface ITagsService
    {
        Task<List<TagEntity>> GetAllTagsAsync();
        Task<TagEntity> GetTagAsync(Guid id);
    }
}