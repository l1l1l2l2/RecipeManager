using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IRecipeService service, ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
        }



        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _service.GetRecipeDetail(id);
            //TODO: Identity on AuthService 
            var creatorId = await _service.GetCreatorId(id);
            if (creatorId == _userManager.GetUserId(User))
                CanEditRecipe = true;

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
