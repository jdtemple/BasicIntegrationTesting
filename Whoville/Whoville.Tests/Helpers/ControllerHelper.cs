using System.Collections.Generic;
using Whoville.Data.Models;

namespace Whoville.Tests.Helpers
{
  public static class ControllerHelper
  {
    internal static List<Story> GetStories(int count = 1)
    {
      var stories = new List<Story>(count);

      for (int i = 0; i < count; i++)
      {
        var story = new Story().RandomizeProperties();

        story.Id = i + 1;

        stories.Add(story);
      }

      var characterId = 0;

      foreach (var story in stories)
      {
        //add some characters to the story
        story.Characters = new List<Character>(count);
        
        for (int i = 0; i < count; i++)
        {
          var character = new Character().RandomizeProperties();

          character.Id = ++characterId;

          story.Characters.Add(character);
        }
      }

      return stories;
    }
  }
}
