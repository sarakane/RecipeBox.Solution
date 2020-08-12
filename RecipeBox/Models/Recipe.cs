using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.Ingredients = new HashSet<RecipeIngredient>();
      this.Tags = new HashSet<TagRecipe>();
    }
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public int Rating { get; set; }
    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<RecipeIngredient> Ingredients { get; set; }
    public virtual ICollection<TagRecipe> Tags { get; set; }

  }
}