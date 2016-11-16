using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;
using Whoville.Data.Repositories;
using Whoville.Tests.Helpers;

namespace Whoville.Tests.IntegrationTests
{
  [TestClass]
  public class StoryRepositoryTest
  {
    private readonly IStoryRepository _storyRepo;
    private RepositoryHelper _repoHelper;

    public StoryRepositoryTest()
    {
      _repoHelper = new RepositoryHelper();
      _storyRepo = new StoryRepository(_repoHelper.Context);
    }
    
    [TestMethod]
    public void StoryRepository_Get()
    {
      //seed a story
      var story = _repoHelper.SeedStories().First();
      
      //get the story
      var storyDb = _storyRepo.Get(story.Id);

      //check to see if the db returns what we gave it
      var comparer = new PropertyComparer<Story>();

      Assert.IsTrue(comparer.Equals(story, storyDb));
    }

    [TestMethod]
    public void StoryRepository_GetAll()
    {
      //seed some stories
      var stories = _repoHelper.SeedStories(3);

      //get all the stories
      var storiesDb = _storyRepo.GetAll();

      //check to see if we got something back
      Assert.IsNotNull(storiesDb);

      //check to see if we got the correct amount back
      Assert.AreEqual(3, storiesDb.Count());
    }

    [TestMethod]
    public void StoryRepository_Get_Includes_Characters()
    {
      //seed a story
      var story = _repoHelper.SeedStories().First();

      //add some characters to the story
      var characters = _repoHelper.SeedCharacters(3, story);

      //get the story
      var storyDb = _storyRepo.Get(story.Id);

      //check to see if the story has characters
      Assert.IsNotNull(storyDb.Characters);

      //check to see if we got the correct number of characters
      Assert.AreEqual(3, storyDb.Characters.Count());
    }

    [TestMethod]
    public void StoryRepository_AddCharacter()
    {
      //seed a story
      var story = _repoHelper.SeedStories().First();

      //add some characters to the story
      var characters = _repoHelper.SeedCharacters(3, story);

      //create a new character
      var character = new Character().RandomizeProperties();

      //add the character to the story
      story.Characters.Add(character);

      //save the modified story
      _storyRepo.Save(story);

      //get the story from the db
      var entityDb = _storyRepo.Get(story.Id);

      //ensure we now have 4 characters
      Assert.IsNotNull(entityDb.Characters);

      Assert.AreEqual(4, entityDb.Characters.Count());
    }
  }
}
