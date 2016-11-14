using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Whoville.Controllers;

namespace Whoville.Tests.Controllers
{
  [TestClass]
  public class HomeControllerTest
  {
    [TestMethod]
    public void HomeController_Index()
    {
      // Arrange
      HomeController controller = new HomeController();

      // Act
      ViewResult result = controller.Index() as ViewResult;

      // Assert
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void HomeController_About()
    {
      // Arrange
      HomeController controller = new HomeController();

      // Act
      ViewResult result = controller.About() as ViewResult;

      // Assert
      Assert.AreEqual("Your application description page.", result.ViewBag.Message);
    }

    [TestMethod]
    public void HomeController_Contact()
    {
      // Arrange
      HomeController controller = new HomeController();

      // Act
      ViewResult result = controller.Contact() as ViewResult;

      // Assert
      Assert.IsNotNull(result);
    }
  }
}
