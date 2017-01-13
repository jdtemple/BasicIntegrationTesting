using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using Whoville.Data;
using Whoville.Data.Migrations;
using Whoville.Data.Models;

namespace Whoville.Tests.Helpers
{
  public class RepositoryHelper : IDisposable
  {
    public WhovilleContext Context { get; private set; }

    private DbContextTransaction _transaction;

    public RepositoryHelper()
    {
      Context = new WhovilleContext("name=WhovilleTestContext");

      ApplyMigrations();

      _transaction = Context.Database.BeginTransaction();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (_transaction != null)
        {
          //roll back the transaction, so the database is clean for the next test run
          _transaction.Rollback();
          _transaction.Dispose();
        }

        if (Context != null)
        {
          Context.Dispose();
        }
      }
    }

    private void ApplyMigrations()
    {
      //update the database schema for any new migrations that took place
      var dbConfig = new Configuration();

      dbConfig.ContextType = typeof(WhovilleContext);
      dbConfig.TargetDatabase = new DbConnectionInfo("WhovilleTestContext");

      var dbMigrator = new DbMigrator(dbConfig);

      if (dbMigrator.GetPendingMigrations().Count() > 0)
      {
        dbMigrator.Update();

        //commit the migration
        Context.SaveChanges();

        //run the seed
        dbConfig.SeedTestDatabase(Context);
      }
    }

    internal List<Story> SeedStories(int count = 1)
    {
      var stories = new List<Story>(count);

      for (int i = 0; i < count; i++)
      {
        stories.Add(new Story().RandomizeProperties());
      }

      try
      {
        Context.Stories.AddRange(stories);
        Context.SaveChanges();

        return stories;
      }
      catch (Exception)
      {
        throw;
      }
    }

    internal List<Character> SeedCharacters(int count = 1, Story story = null)
    {
      if (story == null)
      {
        story = SeedStories().First();
      }

      if (story.Characters == null)
      {
        story.Characters = new List<Character>(count);
      }

      for (int i = 0; i < count; i++)
      {
        var character = new Character().RandomizeProperties();
        
        story.Characters.Add(new Character().RandomizeProperties());
      }

      try
      {
        Context.SaveChanges();

        return story.Characters.ToList();
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
