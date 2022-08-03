using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public bool CanEditRecipe { get; set; }
        private readonly IRecipeService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IRecipeService service, ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, IAuthorizationService authService)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
            _authService = authService;
        }



        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _service.GetRecipeDetail(id);

            if (Recipe is null)
            {
                _logger.LogWarning($"Could not find recipe with id {id}");
                return NotFound();
            }
            var recipe = await _service.GetRecipe(id);
            var authResult = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            CanEditRecipe = authResult.Succeeded;
            return Page();
        }
    }
}
