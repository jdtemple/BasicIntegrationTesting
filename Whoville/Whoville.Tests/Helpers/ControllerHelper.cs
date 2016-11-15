using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        stories.Add(new Story().RandomizeProperties());
      }

      foreach (var story in stories)
      {
        //add some characters to the story
        story.Characters = new List<Character>(count);

        for (int i = 0; i < count; i++)
        {
          story.Characters.Add(new Character().RandomizeProperties());
        }
      }

      return stories;
    }
  }
}
