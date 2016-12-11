using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;

namespace Whoville.Data.Repositories
{
  public class CabinetRepository : ICabinetRepository
  {
    private VaultContext _db;

    public CabinetRepository(VaultContext db)
    {
      _db = db;
    }

    public Cabinet Get(int id)
    {
      return _db.Cabinets
        .Include(x => x.Folders)
        .Single(x => x.Id == id);
    }

    public List<Cabinet> GetAll()
    {
      return _db.Cabinets
        .Include(x => x.Folders)
        .AsNoTracking()
        .ToList();
    }

    public Cabinet Save(Cabinet entity)
    {
      if (entity.Id == 0)
      {
        //new entry
        _db.Cabinets.Add(entity);
      }
      else
      {
        //existing entry
        _db.Cabinets.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
      }

      _db.SaveChanges();

      return entity;
    }
  }
}
