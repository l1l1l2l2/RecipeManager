namespace RecipeManager.Models
{
    public class RecipeSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeToCook { get; set; }
        public int NumberOfIngredients { get; set; }
    }
}
