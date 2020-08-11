using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBox.Controllers
{
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;

    public RecipesController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Recipe> model = _db.Recipes.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Recipe recipe, int TagId)
    {
      _db.Recipes.Add(recipe);
      if  (TagId != 0)
      {
        _db.TagRecipe.Add(new TagRecipe() { TagId = TagId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes;
      return View(thisRecipe);
    }
  }
}