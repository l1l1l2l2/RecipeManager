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
        public Task<int> CreateRecipe(InputRecipe inputRecipe, ApplicationUser user);
        public Task<Recipe> GetRecipe(int id);
        public Task<RecipeDetailViewModel> GetRecipeDetail(int id);
        public Task<InputRecipe> GetRecipeForUpdate(int id);
        public Task UpdateRecipe(InputRecipe inputRecipe ,int id);
        public Task DeleteRecipe(int id);
        public Task<string> GetCreatorId(int id);
        public Task<List<RecipeSummaryViewModel>> GetUserRecipes(string userId);  
    }
}
