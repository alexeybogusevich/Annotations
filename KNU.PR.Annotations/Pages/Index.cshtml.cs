using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.PR.Annotations.Services.TagsService;
using KNU.PR.DbManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KNU.PR.Annotations.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ITagsService tagsService;

        public IndexModel(ILogger<IndexModel> logger, ITagsService tagsService)
        {
            this.logger = logger;
            this.tagsService = tagsService;
        }

        [BindProperty]
        public List<TagEntity> Tags { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Tags = await tagsService.GetAllTagsAsync();
            return Page();
        }
    }
}
