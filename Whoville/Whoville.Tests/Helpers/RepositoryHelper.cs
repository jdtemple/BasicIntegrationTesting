using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Whoville.Data;
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

      _transaction = Context.Database.BeginTransaction();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    
    internal List<Story> SeedStories(int count = 1)
    {
      var entities = new List<Story>(count);

      for (int i = 0; i < count; i++)
      {
        entities.Add(new Story().RandomizeProperties());
      }

      try
      {
        Context.Stories.AddRange(entities);
        Context.SaveChanges();

        return entities;
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
        story.Characters = new List<Character>();
      }

      var characters = new List<Character>(count);

      for (int i = 0; i < count; i++)
      {
        var character = new Character().RandomizeProperties();
        
        story.Characters.Add(new Character().RandomizeProperties());
      }

      try
      {
        Context.SaveChanges();

        return characters;
      }
      catch (Exception)
      {
        throw;
      }
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
  }
}
