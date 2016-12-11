using System.Collections.Generic;
using Whoville.Data.Models;

namespace Whoville.Tests.Helpers
{
  public static class ControllerHelper
  {
    internal static List<Cabinet> GetCabinets(int count = 1)
    {
      var cabinets = new List<Cabinet>(count);

      for (int i = 0; i < count; i++)
      {
        var cabinet = new Cabinet().RandomizeProperties();

        cabinet.Id = i + 1;

        cabinets.Add(cabinet);
      }

      var characterId = 0;

      foreach (var cabinet in cabinets)
      {
        //add some characters to the story
        cabinet.Folders = new List<Folder>(count);
        
        for (int i = 0; i < count; i++)
        {
          var folder = new Folder().RandomizeProperties();

          folder.Id = ++characterId;

          cabinet.Folders.Add(folder);
        }
      }

      return cabinets;
    }
  }
}
