using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;
using Whoville.Data.Repositories;
using Whoville.Tests.Helpers;

namespace Whoville.Tests.IntegrationTests
{
  [TestClass]
  public class CabinetRepositoryTest
  {
    private readonly ICabinetRepository _cabinetRepo;
    private RepositoryHelper _repoHelper;

    public CabinetRepositoryTest()
    {
      _repoHelper = new RepositoryHelper();
      _cabinetRepo = new CabinetRepository(_repoHelper.Context);
    }
    
    [TestMethod]
    public void CabinetRepository_Get()
    {
      //seed a story
      var cabinet = _repoHelper.SeedCabinets().First();
      
      //get the story
      var cabinetDb = _cabinetRepo.Get(cabinet.Id);

      //check to see if the db returns what we gave it
      var comparer = new PropertyComparer<Cabinet>();

      Assert.IsTrue(comparer.Equals(cabinet, cabinetDb));
    }

    [TestMethod]
    public void CabinetRepository_GetAll()
    {
      //seed some stories
      var cabinets = _repoHelper.SeedCabinets(3);

      //get all the stories
      var cabinetsDb = _cabinetRepo.GetAll();

      //check to see if we got something back
      Assert.IsNotNull(cabinetsDb);

      //check to see if we got the correct amount back
      Assert.AreEqual(3, cabinetsDb.Count());
    }

    [TestMethod]
    public void CabinetRepository_Get_Includes_Folders()
    {
      //seed a story
      var cabinet = _repoHelper.SeedCabinets().First();

      //add some characters to the story
      var folders = _repoHelper.SeedFolders(3, cabinet);

      //get the story
      var storyDb = _cabinetRepo.Get(cabinet.Id);

      //check to see if the story has characters
      Assert.IsNotNull(storyDb.Folders);

      //check to see if we got the correct number of characters
      Assert.AreEqual(3, storyDb.Folders.Count());
    }

    [TestMethod]
    public void CabinetRepository_AddFolder()
    {
      //seed a story
      var cabinet = _repoHelper.SeedCabinets().First();

      //add some characters to the story
      var folders = _repoHelper.SeedFolders(3, cabinet);

      //create a new character
      var folder = new Folder().RandomizeProperties();

      //add the character to the story
      cabinet.Folders.Add(folder);

      //save the modified story
      _cabinetRepo.Save(cabinet);

      //get the story from the db
      var entityDb = _cabinetRepo.Get(cabinet.Id);

      //ensure we now have 4 characters
      Assert.IsNotNull(entityDb.Folders);

      Assert.AreEqual(4, entityDb.Folders.Count());
    }
  }
}
