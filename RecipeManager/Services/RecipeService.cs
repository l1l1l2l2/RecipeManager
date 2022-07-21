using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Models;
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
    }
}
