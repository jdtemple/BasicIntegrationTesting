using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;
using Whoville.Data.Repositories;
using Whoville.Tests.Helpers;

namespace Whoville.Tests.IntegrationTests
{
  [TestClass]
  public class FolderRepositoryTest
  {
    private readonly IFolderRepository _folderRepo;
    private RepositoryHelper _repoHelper;

    public FolderRepositoryTest()
    {
      _repoHelper = new RepositoryHelper();
      _folderRepo = new FolderRepository(_repoHelper.Context);
    }

    [TestMethod]
    public void FolderRepository_Get()
    {
      //seed a folder
      var folder = _repoHelper.SeedFolders().First();

      //get the folder
      var folderDb = _folderRepo.Get(folder.Id);

      //check to see if the db returns what we gave it
      var comparer = new PropertyComparer<Folder>();

      Assert.IsTrue(comparer.Equals(folder, folderDb));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "A Folder was saved without a Cabinet.")]
    public void FolderRepository_NewNoCabinet()
    {
      //create a folder
      var folder = new Folder
      {
        Name = Guid.NewGuid().ToString(),
      };

      //save without a cabinet (exception)
      _folderRepo.Save(folder);
    }

    [TestMethod]
    public void FolderRepository_New()
    {
      //seed a cabinet
      var cabinet = _repoHelper.SeedCabinets().First();

      //create a folder
      var folder = new Folder
      {
        Name = Guid.NewGuid().ToString(),
        Cabinet = cabinet
      };

      //save the folder
      _folderRepo.Save(folder);

      //get the folder from the db
      var folderDb = _folderRepo.Get(folder.Id);

      //ensure the db returns what we gave it
      var comparer = new PropertyComparer<Folder>();

      Assert.IsTrue(comparer.Equals(folder, folderDb));
    }

    [TestMethod]
    public void FolderRepository_Save()
    {
      //seed a folder
      var folder = _repoHelper.SeedFolders().First();

      //keep a local copy of the folder name
      var folderInitName = folder.Name;

      //modify the folder
      folder.Name = Guid.NewGuid().ToString();

      //keep a local copy of the new folder name
      var folderModName = folder.Name;

      //save the folder with the new name
      _folderRepo.Save(folder);

      //get the folder from the db
      var folderDb = _folderRepo.Get(folder.Id);

      //ensure the init name isn't the same as the db name
      Assert.AreNotEqual(folderInitName, folderDb.Name);

      //ensure the mod name is the same as the db name
      Assert.AreEqual(folderModName, folderDb.Name);
      
      //ensure the db returns what we gave it
      var comparer = new PropertyComparer<Folder>();

      Assert.IsTrue(comparer.Equals(folder, folderDb));
    }
  }
}
