using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Services;
using System.Threading.Tasks;

namespace RecipeManager.Pages.Recipes
{
    [Authorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public InputRecipe Input { get; set; }
        public int Id { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly IRecipeService _service;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IAuthorizationService _authService;
        public EditModel(
            ILogger<IndexModel> logger,
            IRecipeService service,
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authService)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
            _authService = authService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id;
            var recipe = await _service.GetRecipe(id);
            if (recipe is null)
            {
                _logger.LogWarning($"Could not find recipe with id {id}");
                return NotFound();
            }

            var authResult = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            if (authResult.Succeeded)
            {
                Input = await _service.GetRecipeForUpdate(id);
                return Page();
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var recipe = await _service.GetRecipe(id);
            var authResult = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            if (recipe is null)
            {
                _logger.LogWarning($"Could not find recipe with id {id}");
                return NotFound();
            }
            if (authResult.Succeeded)
            {
                await _service.DeleteRecipe(id);
                return RedirectToPage("/Index");
            }
            return new ForbidResult();

        }
        public async Task<IActionResult> OnPost(int id)
        {
            var recipe = await _service.GetRecipe(id);
            if (recipe is null)
            {
                _logger.LogWarning($"Could not find recipe with id {id}");
                return NotFound();
            }
            var authResult = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            if (ModelState.IsValid && authResult.Succeeded)
            {
                await _service.UpdateRecipe(Input, id);
                return RedirectToPage("Index", new { id });
            }
            return Page();
        }
    }
}
