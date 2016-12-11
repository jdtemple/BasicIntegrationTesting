using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;

namespace Whoville.Data.Repositories
{
  public class FileRepository : IFileRepository
  {

    private VaultContext _db;

    public FileRepository(VaultContext db)
    {
      _db = db;
    }

    public File Get(int id)
    {
      return _db.Files.Single(x => x.Id == id);
    }

    public File Save(File entity)
    {
      if (entity.Folder == null)
      {
        throw new ArgumentException("A File requires a Folder.");
      }

      if (entity.Id == 0)
      {
        //new entry
        _db.Files.Add(entity);
      }
      else
      {
        //existing entry
        _db.Files.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
      }

      _db.SaveChanges();

      return entity;
    }
  }
}
