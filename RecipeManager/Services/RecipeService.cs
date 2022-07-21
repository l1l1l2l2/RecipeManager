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
        readonly ILogger _logger;
        public RecipeService(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
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
