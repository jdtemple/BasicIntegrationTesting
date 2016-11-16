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
    private readonly IStoryRepository _storyRepo;

    private readonly List<Story> _stories;

    public HomeControllerTest()
    {
      //get the mock stories
      _stories = ControllerHelper.GetStories(3);

      //mock the story repo
      var storyRepo = new Mock<IStoryRepository>();

      //mock the repo's GetAll method
      storyRepo.Setup(x => x.GetAll())
        .Returns(_stories);

      //hook up the private mock repo
      _storyRepo = storyRepo.Object;

      //register automapper maps
      MapperConfig.RegisterMaps();
    }

    [TestMethod]
    public void HomeController_Index()
    {
      //controller instance with dependency injection
      var controller = new HomeController(_storyRepo);

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
      Assert.IsInstanceOfType(viewResult.Model, typeof(List<StoryModel>));

      var model = viewResult.Model as List<StoryModel>;

      //ensure the model matches our mock
      Assert.AreEqual(_stories.Count, model.Count);

      var comparer = new PropertyComparer<StoryModel>();
      var storyModels = Mapper.Map<List<Story>, List<StoryModel>>(_stories);

      foreach (var modelItem in model)
      {
        var mockItem = storyModels.Single(x => x.Id == modelItem.Id);

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
