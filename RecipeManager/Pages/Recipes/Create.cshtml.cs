using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
        public InputRecipe Input { get; set; }
        public ILogger<IndexModel> _logger { get; private set; }
        public IRecipeService _service { get; private set; }
        public CreateModel(ILogger<IndexModel> logger, IRecipeService service)
        {
            _logger = logger;
            _service = service;
        }

        

        public void OnGet()
        {
        }
        public void OnPost()
        {

        }
    }

    public class InputRecipe
    {
        [Required]
        public string Name { get; set; }
        [Range(0, 24), DisplayName("Time to cook (hrs)")]
        public int TimeToCookHrs { get; set; }
        [Range(0, 59), DisplayName("Time to cook (mins)")]
        public int TimeToCookMins { get; set; }
        public string Method { get; set; }
        [DisplayName("Vegetarian")]
        public bool IsVegetarian { get; set; }
        List<InputIngredient> Ingredients { get; set; } = new List<InputIngredient>();

    }
    public class InputIngredient
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Quantity { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
    }
}
