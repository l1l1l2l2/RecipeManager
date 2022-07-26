using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Models;
using RecipeManager.Pages.Recipes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Services
{
    public class RecipeService : IRecipeService
    {
        readonly ApplicationDbContext _context;
        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<RecipeSummaryViewModel>> GetAllRecipes()
        {
            return _context.Recipes
                .Where(x => !x.IsDeleted)
                .Select(x => new RecipeSummaryViewModel
                {
                    Id = x.RecipeId,
                    Name = x.Name,
                    TimeToCook = $"{x.TimeToCook.Hours}hrs {x.TimeToCook.Minutes}mins"
                })
                .ToListAsync();
        }
        public async Task<int> CreateRecipe(InputRecipe inputRecipe, ApplicationUser user)
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
            await _context.SaveChangesAsync();
            return recipe.RecipeId;
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            return await _context.Recipes
                .FirstOrDefaultAsync(x => x.RecipeId == id);
        }

        public async Task<RecipeDetailViewModel> GetRecipeDetail(int id)
        {
            return await _context.Recipes
                .Where(x => !x.IsDeleted && x.RecipeId == id)
                .Select(x => new RecipeDetailViewModel
                {
                    Id = id,
                    Name = x.Name,
                    Method = x.Method,
                    TimeToCook = $"{x.TimeToCook.Hours}hrs {x.TimeToCook.Minutes}min",
                    ingredients = x.Ingredients
                        .Select(y => new IngredientSummary
                        {
                            Name = y.Name,
                            Quantity = y.Quantity.ToString(),
                            Unit = y.Unit
                        }),
                    
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InputRecipe> GetRecipeForUpdate(int id)
        {
            //var recipe = await _context.Recipes
            //    .FirstOrDefaultAsync(x => x.RecipeId == id); ;
            //var ingredients = recipe.Ingredients
            //        .Select(x => new InputIngredient
            //        {
            //            Name = x.Name,
            //            Quantity = x.Quantity,
            //            Unit = x.Unit

            //        }).ToList();
            //var recipe = await _context.Recipes
            //    .Where(x => x.RecipeId == id && !x.IsDeleted)
            //    .FirstOrDefaultAsync();
            return await _context.Recipes
                .Where(x => x.RecipeId == id && !x.IsDeleted)
                .Select(recipe => new InputRecipe
                {
                    Name = recipe.Name,
                    Method = recipe.Method,
                    IsVegetarian = recipe.IsVegetarian,
                    TimeToCookHrs = recipe.TimeToCook.Hours,
                    TimeToCookMins = recipe.TimeToCook.Minutes,
                    Ingredients = recipe.Ingredients
                    .Select(x => new InputIngredient
                    {
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Unit = x.Unit

                    }).ToList()
                })
                .FirstOrDefaultAsync();

        }
        public async Task UpdateRecipe(InputRecipe inputRecipe, int id)
        {
            var recipe = await _context.Recipes
                .Where(x => x.RecipeId == id && !x.IsDeleted)
                .Include(x=> x.Ingredients)
                .FirstOrDefaultAsync();


            recipe.Name = inputRecipe.Name;
            recipe.Method = inputRecipe.Method;
            recipe.IsVegetarian = inputRecipe.IsVegetarian;
            recipe.TimeToCook = new System.TimeSpan(inputRecipe.TimeToCookHrs, inputRecipe.TimeToCookMins, 0);
            recipe.Ingredients = inputRecipe?.Ingredients
                .Select(x => new Ingredient()
                {
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Unit = x.Unit
                }).ToList();

            _context.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes
                .FindAsync(id);
            if (recipe is not null) 
            { 
                recipe.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetCreatorId(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            return recipe.CreatedById;
        }

        public async Task<List<RecipeSummaryViewModel>> GetUserRecipes(string userId)
        {
            return await _context.Recipes
                .Where(x => !x.IsDeleted && x.CreatedById == userId)
                .Select(x => new RecipeSummaryViewModel
                {
                    Id = x.RecipeId,
                    Name = x.Name,
                    TimeToCook = $"{x.TimeToCook.Hours}hrs {x.TimeToCook.Minutes}mins"
                })
                .ToListAsync();
        }
    }
}
