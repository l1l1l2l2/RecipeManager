using System;
using System.Collections.Generic;

namespace RecipeManager.Data
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public TimeSpan TimeToCook { get; set; }
        public bool IsDeleted { get; set; }
        public string Method { get; set; }
        public bool IsVegetarian { get; set; }
        public string CreatedById { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set;}
    }
}
