using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Whoville.App_Start;
using Whoville.Controllers;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;
using Whoville.Data.ViewModels;
using Whoville.Tests.Helpers;

namespace Whoville.Tests.UnitTests
{
  [TestClass]
  public class HomeControllerTest
  {
    private readonly ICabinetRepository _cabinetRepo;

    private readonly List<Cabinet> _cabinets;

    public HomeControllerTest()
    {
      //get the mock stories
      _cabinets = ControllerHelper.GetCabinets(3);

      //mock the story repo
      var cabinetRepo = new Mock<ICabinetRepository>();

      //mock the repo's GetAll method
      cabinetRepo.Setup(x => x.GetAll())
        .Returns(_cabinets);

      //hook up the private mock repo
      _cabinetRepo = cabinetRepo.Object;

      //register automapper maps
      MapperConfig.RegisterMaps();
    }

    [TestMethod]
    public void HomeController_Index()
    {
      //controller instance with dependency injection
      var controller = new HomeController(_cabinetRepo);

      //fire the index action
      var result = controller.Index() as ViewResult;

      //ensure we get something back
      Assert.IsNotNull(result);

      //ensure result is a ViewResult
      Assert.IsInstanceOfType(result, typeof(ViewResult));

      var viewResult = result as ViewResult;

      //ensure viewResult is the index view
      Assert.AreEqual("Index", viewResult.ViewName);

      //ensure the model is a story collection
      Assert.IsInstanceOfType(viewResult.Model, typeof(List<CabinetModel>));

      var model = viewResult.Model as List<CabinetModel>;

      //ensure the model matches our mock
      Assert.AreEqual(_cabinets.Count, model.Count);

      var comparer = new PropertyComparer<CabinetModel>();
      var cabinetModels = Mapper.Map<List<Cabinet>, List<CabinetModel>>(_cabinets);

      foreach (var modelItem in model)
      {
        var mockItem = cabinetModels.Single(x => x.Id == modelItem.Id);

        Assert.IsTrue(comparer.Equals(modelItem, mockItem));
      }
    }

    //[TestMethod]
    //public void HomeController_About()
    //{
    //  // Arrange
    //  HomeController controller = new HomeController();

    //  // Act
    //  ViewResult result = controller.About() as ViewResult;

    //  // Assert
    //  Assert.AreEqual("Your application description page.", result.ViewBag.Message);
    //}

    //[TestMethod]
    //public void HomeController_Contact()
    //{
    //  // Arrange
    //  HomeController controller = new HomeController();

    //  // Act
    //  ViewResult result = controller.Contact() as ViewResult;

    //  // Assert
    //  Assert.IsNotNull(result);
    //}
  }
}
