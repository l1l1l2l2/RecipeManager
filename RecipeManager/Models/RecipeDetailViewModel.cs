using System.Collections.Generic;

namespace RecipeManager.Models
{
    public class RecipeDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeToCook { get; set; }
        public string Method { get; set; }
        public IEnumerable<IngredientSummary> ingredients { get; set; }
    }
    public class IngredientSummary
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
    }
}
