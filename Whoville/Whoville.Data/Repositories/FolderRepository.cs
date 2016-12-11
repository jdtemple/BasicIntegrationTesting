using System;
using System.Data.Entity;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;

namespace Whoville.Data.Repositories
{
  public class FolderRepository : IFolderRepository
  {
    private VaultContext _db;

    public FolderRepository(VaultContext db)
    {
      _db = db;
    }

    public Folder Get(int id)
    {
      return _db.Folders.Single(x => x.Id == id);
    }

    public Folder Save(Folder entity)
    {
      if (entity.Cabinet == null)
      {
        throw new ArgumentException("A Folder requires a Cabinet.");
      }
      
      if (entity.Id == 0)
      {
        //new entry
        _db.Folders.Add(entity);
      }
      else
      {
        //existing entry
        _db.Folders.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
      }

      _db.SaveChanges();

      return entity;
    }
  }
}
