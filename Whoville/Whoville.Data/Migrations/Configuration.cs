using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Whoville.Data.Models;

namespace Whoville.Data.Migrations
{
  public sealed class Configuration : DbMigrationsConfiguration<WhovilleContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    public void SeedTestDatabase(WhovilleContext context)
    {
      if (context.Database.Connection.Database.Equals("Whoville_Tests"))
      {
        Seed(context);
      }
      else
      {
        throw new ArgumentException("SeedTestDatabase only allowed on the test database instance");
      }
    }

    protected override void Seed(WhovilleContext db)
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

    private void SeedStories(WhovilleContext db)
    {
      try
      {
        var storiesExist = db.Stories.Any();

        if (!storiesExist)
        {
          //add a story with some characters so we have something to start with
          var story = new Story
          {
            Characters = new List<Character>(),
            Name = "How the Grinch Stole Christmas!"
          };

          var grinch = new Character
          {
            Name = "Grinch"
          };

          var cindyLou = new Character
          {
            Name = "Cindy Lou Who"
          };

          story.Characters.Add(grinch);
          story.Characters.Add(cindyLou);

          db.Stories.Add(story);

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
