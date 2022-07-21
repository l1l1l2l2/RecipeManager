using System.Collections.Generic;
using RecipeManager.Models;

namespace RecipeManager.Services
{
    public interface IRecipeService
    {
        public List<RecipeSummaryViewModel> GetAllRecipes();
    }
}
