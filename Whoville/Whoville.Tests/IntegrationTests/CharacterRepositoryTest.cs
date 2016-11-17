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
  public class CharacterRepositoryTest
  {
    private readonly ICharacterRepository _characterRepo;
    private RepositoryHelper _repoHelper;

    public CharacterRepositoryTest()
    {
      _repoHelper = new RepositoryHelper();
      _characterRepo = new CharacterRepository(_repoHelper.Context);
    }

    [TestMethod]
    public void CharacterRepository_Get()
    {
      //seed a character
      var character = _repoHelper.SeedCharacters().First();

      //get the character
      var characterDb = _characterRepo.Get(character.Id);

      //check to see if the db returns what we gave it
      var comparer = new PropertyComparer<Character>();

      Assert.IsTrue(comparer.Equals(character, characterDb));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "A Character was saved without a Story.")]
    public void CharacterRepository_NewNoStory()
    {
      //create a character
      var character = new Character
      {
        Name = Guid.NewGuid().ToString(),
      };

      //save without a story (exception)
      _characterRepo.Save(character);
    }

    [TestMethod]
    public void CharacterRepository_New()
    {
      //seed a story
      var story = _repoHelper.SeedStories().First();

      //create a character
      var character = new Character
      {
        Name = Guid.NewGuid().ToString(),
        Story = story
      };

      //save the character
      _characterRepo.Save(character);

      //get the character from the db
      var characterDb = _characterRepo.Get(character.Id);

      //ensure the db returns what we gave it
      var comparer = new PropertyComparer<Character>();

      Assert.IsTrue(comparer.Equals(character, characterDb));
    }

    [TestMethod]
    public void CharacterRepository_Save()
    {
      //seed a character
      var character = _repoHelper.SeedCharacters().First();

      //keep a local copy of the character name
      var characterInitName = character.Name;

      //modify the character
      character.Name = Guid.NewGuid().ToString();

      //keep a local copy of the new character name
      var characterModName = character.Name;

      //save the character with the new name
      _characterRepo.Save(character);

      //get the character from the db
      var characterDb = _characterRepo.Get(character.Id);

      //ensure the init name isn't the same as the db name
      Assert.AreNotEqual(characterInitName, characterDb.Name);

      //ensure the mod name is the same as the db name
      Assert.AreEqual(characterModName, characterDb.Name);
      
      //ensure the db returns what we gave it
      var comparer = new PropertyComparer<Character>();

      Assert.IsTrue(comparer.Equals(character, characterDb));
    }
  }
}
