using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBox.Controllers
{
  public class TagsController : Controller
  {
    private readonly RecipeBoxContext _db;

    public TagsController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Tag> model = _db.Tags.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Tag tag)
    {
      _db.Tags.Add(tag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTag = _db.Tags
        .Include(tag => tag.Recipes)
        .ThenInclude(join => join.Recipe)
        .FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

    public ActionResult Edit(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId ==id);
      return View(thisTag);
    }

    [HttpPost]
    public ActionResult Edit (Tag tag)
    {
      _db.Entry(tag).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId ==id);
      return View(thisTag);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
      _db.Tags.Remove(thisTag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View(thisTag);
    }

    [HttpPost]
    public ActionResult AddRecipe(Tag tag, int RecipeId)
    {
      if (RecipeId != 0 && !_db.TagRecipe.Any(x => x.TagId == tag.TagId && x.RecipeId == RecipeId))
      {
        _db.TagRecipe.Add(new TagRecipe() { RecipeId = RecipeId, TagId = tag.TagId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    [HttpPost]
    public ActionResult RemoveRecipe(int joinId)
    {
      var joinEntry = _db.TagRecipe.FirstOrDefault(join => join.TagRecipeId == joinId);
      _db.TagRecipe.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}