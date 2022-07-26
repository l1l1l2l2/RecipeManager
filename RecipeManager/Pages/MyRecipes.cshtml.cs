using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Models;
using RecipeManager.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManager.Pages
{
    [Authorize]
    public class MyRecipesModel : PageModel
    {
        private readonly IRecipeService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<IndexModel> _logger;
        public List<RecipeSummaryViewModel> Recipes { get; private set; }

        public MyRecipesModel(IRecipeService service, ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            Recipes = await _service.GetUserRecipes(_userManager.GetUserId(User));
            return Page();  
        }
    }
}
