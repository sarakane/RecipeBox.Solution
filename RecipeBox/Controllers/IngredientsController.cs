using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class IngredientsController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public IngredientsController(UserManager<ApplicationUser> userManager,RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userIngredients = _db.Ingredients.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userIngredients);
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
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(Ingredient ingredient)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      ingredient.User = currentUser;
      _db.Ingredients.Add(ingredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredients => ingredients.IngredientId == id);
      return View(thisIngredient);
    }
    [HttpPost]
    public ActionResult Edit(Ingredient ingredient)
    {
      _db.Entry(ingredient).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ingredient.IngredientId });
    }

    public ActionResult Delete(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredients => ingredients.IngredientId ==id);
      return View(thisIngredient);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredients => ingredients.IngredientId == id);
      _db.Ingredients.Remove(thisIngredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteRecipe(int joinId)
    {
      var joinEntry = _db.RecipeIngredient.FirstOrDefault(entry => entry.RecipeIngredientId == joinId);
      _db.RecipeIngredient.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredients => ingredients.IngredientId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View(thisIngredient);
    }

    [HttpPost]
    public ActionResult AddRecipe(Ingredient ingredient, int RecipeId)
    {     
      if (RecipeId != 0 && !_db.RecipeIngredient.Any(x => x.RecipeId == RecipeId && x.IngredientId == ingredient.IngredientId))
      {
        _db.RecipeIngredient.Add(new RecipeIngredient() { RecipeId = RecipeId, IngredientId = ingredient.IngredientId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ingredient.IngredientId});
    }
  }
}