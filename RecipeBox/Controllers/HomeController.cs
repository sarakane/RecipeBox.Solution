using Microsoft.AspNetCore.Mvc;

namespace RecipeBox.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

  }
}