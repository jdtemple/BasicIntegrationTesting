using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;
using Whoville.Data.ViewModels;

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
      var stories = _storyRepo.GetAll();

      var storyModels = Mapper.Map<List<Story>, List<StoryModel>>(stories);

      return View("Index", storyModels);
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
