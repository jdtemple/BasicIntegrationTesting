using System.Web.Mvc;
using Whoville.Data.Interfaces;
using Whoville.Data.Repositories;

namespace Whoville.Controllers
{
  public class HomeController : Controller
  {
    private IStoryRepository _storyRepo;

    public HomeController(IStoryRepository storyRepo)
    {
      _storyRepo = storyRepo;
    }

    public ActionResult Index()
    {
      var entities = _storyRepo.GetAll();

      return View("Index", entities);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}
