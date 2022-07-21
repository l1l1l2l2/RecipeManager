using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RecipeService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IRecipeService service)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
