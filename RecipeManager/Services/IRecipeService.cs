using System.Collections.Generic;
using RecipeManager.Data;
using RecipeManager.Models;
using RecipeManager.Pages.Recipes;

namespace RecipeManager.Services
{
    public interface IRecipeService
    {
        public List<RecipeSummaryViewModel> GetAllRecipes();
        public void CreateRecipe(InputRecipe inputRecipe, ApplicationUser user);
    }
}
