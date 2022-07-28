using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Models;
using RecipeManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Pages.Recipes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public InputRecipe Input { get; set; } = new InputRecipe();
        private readonly ILogger<IndexModel> _logger;
        private readonly IRecipeService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(
            ILogger<IndexModel> logger,
            IRecipeService service,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var id = await _service.CreateRecipe(Input, user);
                return RedirectToPage("Index", new { id = id });
            }
            // TODO: log
            return Page();
        }
    }

    public class InputRecipe
    {
        [Required]
        [DisplayName("Name")]
        public string RecipeName { get; set; }
        [Range(0, 24), DisplayName("Time to cook (hrs)")]
        public int TimeToCookHrs { get; set; }
        [Range(0, 59), DisplayName("Time to cook (mins)")]
        public int TimeToCookMins { get; set; }
        public string Method { get; set; }
        [DisplayName("Vegetarian")]
        public bool IsVegetarian { get; set; }
        public List<InputIngredient> Ingredients { get; set; } = new List<InputIngredient>();

    }
    public class InputIngredient
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        public float Quantity { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
    }
}
