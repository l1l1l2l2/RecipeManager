﻿namespace RecipeManager.Data
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

    }
}
