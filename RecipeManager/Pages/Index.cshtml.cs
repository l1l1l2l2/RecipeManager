using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeManager.Models;
using RecipeManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRecipeService _service;
        private readonly ILogger<IndexModel> _logger;

        public List<RecipeSummaryViewModel> Recipes { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, RecipeService service)
        {
            _logger = logger;
            _service = service;
        }

        public void OnGet()
        {
            Recipes = _service.GetAllRecipes();
        }
    }
}
