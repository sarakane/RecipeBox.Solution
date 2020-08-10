using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Ingredient
  {
    public Ingredient()
    {
      this.Recipes = new HashSet<RecipeIngredient>();
    }

    public int IngredientId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<RecipeIngredient> Recipes { get; set; }  
  }
}