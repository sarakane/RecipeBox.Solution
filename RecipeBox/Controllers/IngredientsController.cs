using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Controllers
{
  public class IngredientsController : Controller
  {
    private readonly RecipeBoxContext _db;
    public IngredientsController(RecipeBoxContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Ingredient> model = _db.Ingredients.ToList();
      return View(model);
    }
    public ActionResult Details(int id)
    {
      var thisIngredient = _db.Ingredients
          .Include(ingredient => ingredient.Recipes)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }
    public ActionResult Create()
    {
      
    }
  }
}