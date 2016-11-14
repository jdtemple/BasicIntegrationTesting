using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;
using Whoville.Tests.Helpers;

namespace Whoville.Tests.Repositories
{
  [TestClass]
  public class StoryRepositoryTest
  {
    private readonly IStoryRepository _storyRepo;

    public StoryRepositoryTest()
    {
      //create some mock stories
      var stories = new List<Story>
      {
        new Story().RandomizeProperties(),
        new Story().RandomizeProperties(),
        new Story().RandomizeProperties()
      };

      //create some mock characters for each story
      foreach (var s in stories)
      {
        s.Characters = new List<Character>();

        s.Characters.Add(new Character().RandomizeProperties());
        s.Characters.Add(new Character().RandomizeProperties());
        s.Characters.Add(new Character().RandomizeProperties());
      }

      //mock the story repository
      Mock<IStoryRepository> storyRepo = new Mock<IStoryRepository>();

      //return all the stories
      storyRepo.Setup(x => x.GetAll())
        .Returns(stories);

      //hook up the private repo
      _storyRepo = storyRepo.Object;
    }

    [TestMethod]
    public void StoryRepository_GetAll()
    {
      //get all the stories
      var entities = _storyRepo.GetAll();

      //check to see if we got something back
      Assert.IsNotNull(entities);

      //check to see if we got the correct amount back
      Assert.AreEqual(3, entities.Count());
    }

    [TestMethod]
    public void StoryRepository_GetAll_Includes_Characters()
    {
      //get all the stories
      var entities = _storyRepo.GetAll();

      //check to see if every story has characters
      foreach (var entity in entities)
      {
        //check to see if we got the characters
        Assert.IsNotNull(entity.Characters);

        //check to see if we got the correct number of characters
        Assert.AreEqual(3, entity.Characters.Count());
      }
    }

    [TestMethod]
    public void StoryRepository_AddCharacter()
    {
      //get one story
      var entity = _storyRepo.GetAll()[1];
      
      //create a new character
      var character = new Character().RandomizeProperties();

      //add the character to the story and save
      entity.Characters.Add(character);
      
      _storyRepo.Save(entity);

      //get the story again
      var entityDb = _storyRepo.GetAll()[1];

      //ensure we now have 4 characters
      Assert.IsNotNull(entityDb.Characters);

      Assert.AreEqual(4, entityDb.Characters.Count());
    }
  }
}
