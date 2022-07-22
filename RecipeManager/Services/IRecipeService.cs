using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeManager.Data;
using RecipeManager.Models;
using RecipeManager.Pages.Recipes;

namespace RecipeManager.Services
{
    public interface IRecipeService
    {
        public List<RecipeSummaryViewModel> GetAllRecipes();
        public Task CreateRecipe(InputRecipe inputRecipe, ApplicationUser user);
        public Task<Recipe> GetRecipe(int id);
        public Task<RecipeDetailViewModel> GetRecipeDetail(int id);
    }
}
