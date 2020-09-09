using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.PR.Annotations.Services.NewsService;
using KNU.PR.DbManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KNU.PR.Annotations.Pages
{
    public class NewsByTagModel : PageModel
    {
        private readonly ILogger<NewsByTagModel> logger;
        private readonly INewsService newsService;

        public NewsByTagModel(ILogger<NewsByTagModel> logger, INewsService newsService)
        {
            this.logger = logger;
            this.newsService = newsService;
        }

        [BindProperty]
        public List<NewsEntity> NewsEntities { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid tagId)
        {
            NewsEntities = await newsService.GetNewsByTagAsync(tagId);
            return Page();
        }

        public IActionResult OnPostReturnToList()
        {
            return RedirectToPage("Index");
        }
    }
}
