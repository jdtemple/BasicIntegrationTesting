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
    private ICabinetRepository _cabinetRepo;

    public HomeController(ICabinetRepository cabinetRepo)
    {
      _cabinetRepo = cabinetRepo;
    }

    public ActionResult Index()
    {
      var stories = _cabinetRepo.GetAll();

      var storyModels = Mapper.Map<List<Cabinet>, List<CabinetModel>>(stories);

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
