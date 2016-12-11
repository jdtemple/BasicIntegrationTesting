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
    public VaultContext Context { get; private set; }

    private DbContextTransaction _transaction;

    public RepositoryHelper()
    {
      Context = new VaultContext("VaultTestContext");

      _transaction = Context.Database.BeginTransaction();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    
    internal List<Cabinet> SeedCabinets(int count = 1)
    {
      var cabinets = new List<Cabinet>(count);

      for (int i = 0; i < count; i++)
      {
        cabinets.Add(new Cabinet().RandomizeProperties());
      }

      try
      {
        Context.Cabinets.AddRange(cabinets);
        Context.SaveChanges();

        return cabinets;
      }
      catch (Exception)
      {
        throw;
      }
    }

    internal List<Folder> SeedFolders(int count = 1, Cabinet cabinet = null)
    {
      if (cabinet == null)
      {
        cabinet = SeedCabinets().First();
      }

      if (cabinet.Folders == null)
      {
        cabinet.Folders = new List<Folder>(count);
      }

      for (int i = 0; i < count; i++)
      {
        var folder = new Folder().RandomizeProperties();
        
        cabinet.Folders.Add(new Folder().RandomizeProperties());
      }

      try
      {
        Context.SaveChanges();

        return cabinet.Folders.ToList();
      }
      catch (Exception)
      {
        throw;
      }
    }


    internal List<File> SeedFiles(int count = 1, Cabinet cabinet = null)
    {
      if (cabinet == null)
      {
        cabinet = SeedCabinets().First();
      }

      if (cabinet.Folders == null)
      {
        cabinet.Folders = new List<Folder>(count);
      }
      
      cabinet.Folders.Add(new Folder().RandomizeProperties());

      for(int i = 0; i < count; i++)
      {
        cabinet.Folders.First().Files = new List<File>();
        cabinet.Folders.First().Files.Add(new File().RandomizeProperties());
      }

      try
      {
        Context.SaveChanges();

        return cabinet.Folders.First().Files.ToList();
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
