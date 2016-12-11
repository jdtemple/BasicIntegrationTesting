using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Whoville.Data.Models;

namespace Whoville.Data.Migrations
{
  internal sealed class Configuration : DbMigrationsConfiguration<VaultContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(VaultContext db)
    {
      ////How to debug the seed
      ////1. uncomment the if block below
      ////2. launch a new instance of VS and open this project
      ////3. place your breakpoint(s) in the new instance of VS
      ////4. run update-database in the package manager console in this instance of VS
      ////5. when prompted, select the other VS instance for debugging
      //if (!Debugger.IsAttached)
      //{
      //  Debugger.Launch();
      //}

      SeedStories(db);
    }

    private void SeedStories(VaultContext db)
    {
      try
      {
        var storiesExist = db.Cabinets.Any();

        if (!storiesExist)
        {
          //add a story with some characters so we have something to start with
          var story = new Cabinet
          {
            Folders = new List<Folder>(),
            Name = "How the Grinch Stole Christmas!"
          };

          var grinch = new Folder
          {
            Name = "Grinch"
          };

          var cindyLou = new Folder
          {
            Name = "Cindy Lou Who"
          };

          story.Folders.Add(grinch);
          story.Folders.Add(cindyLou);

          db.Cabinets.Add(story);

          db.SaveChanges();
        }
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
