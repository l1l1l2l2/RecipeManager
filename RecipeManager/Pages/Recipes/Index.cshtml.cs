using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Models;
using RecipeManager.Services;

namespace RecipeManager.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        public RecipeDetailViewModel Recipe { get; set; }
        private readonly IRecipeService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IRecipeService service, ILogger<IndexModel> logger)
        {
            _service = service;
            _logger = logger;
        }

     

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _service.GetRecipeDetail(id);

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
