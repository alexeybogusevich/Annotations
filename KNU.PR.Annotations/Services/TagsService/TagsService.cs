using KNU.PR.DbManager.Connections;
using KNU.PR.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.PR.Annotations.Services.TagsService
{
    public class TagsService : ITagsService
    {
        private readonly AzureSqlDbContext context;

        public TagsService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<List<TagEntity>> GetAllTagsAsync()
        {
            return await context.Tags.ToListAsync();
        }

        public async Task<TagEntity> GetTagAsync(Guid id)
        {
            return await context.Tags.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }
    }
}
