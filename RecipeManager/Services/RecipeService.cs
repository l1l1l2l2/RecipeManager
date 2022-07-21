using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Models;
using RecipeManager.Pages.Recipes;
using System.Collections.Generic;
using System.Linq;

namespace RecipeManager.Services
{
    public class RecipeService : IRecipeService
    {
        readonly ApplicationDbContext _context;
        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<RecipeSummaryViewModel> GetAllRecipes()
        {
            return _context.Recipes
                .Where(x => !x.IsDeleted)
                .Select(x => new RecipeSummaryViewModel
                {
                    Id = x.RecipeId,
                    Name = x.Name,
                    TimeToCook = $"{x.TimeToCook.Hours}hrs {x.TimeToCook.Minutes}mins"
                })
                .ToList();
        }
        public void CreateRecipe(InputRecipe inputRecipe, ApplicationUser user)
        {
            var recipe = new Recipe()
            {
                CreatedById = user.Id,
                Name = inputRecipe.Name,
                Method = inputRecipe.Method,
                IsVegetarian = inputRecipe.IsVegetarian,
                TimeToCook = new System.TimeSpan(inputRecipe.TimeToCookHrs, inputRecipe.TimeToCookMins, 0),
                Ingredients = inputRecipe?.Ingredients
                    .Select(x => new Ingredient()
                    {
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Unit = x.Unit
                    }).ToList()
            };
            _context.Add(recipe);
            _context.SaveChanges();
        }
    }
}
