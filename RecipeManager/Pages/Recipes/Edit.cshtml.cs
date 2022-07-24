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
        public EditModel(
            ILogger<IndexModel> logger,
            IRecipeService service,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var recipe =await _service.GetRecipe(id);
            Id = id;
            if (_userManager.GetUserId(User) == recipe?.CreatedById)
            {
                Input = await _service.GetRecipeForUpdate(id);
                return Page();
            }

            return BadRequest();
        }
        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var recipe = await _service.GetRecipe(id);
            if (_userManager.GetUserId(User) == recipe?.CreatedById)
            {
                await _service.DeleteRecipe(id);
                return RedirectToPage("/Index");
            }
            return new ForbidResult();
            
        }   
        public async Task<IActionResult> OnPost(int id)
        {
            var recipe = await _service.GetRecipe(id);
            if (ModelState.IsValid && _userManager.GetUserId(User) == recipe?.CreatedById)
            {
                await _service.UpdateRecipe(Input, id);
                return RedirectToPage("Index", new { id = id });
            }
            return Page();
        }
    }
}
