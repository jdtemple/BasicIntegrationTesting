using System;
using System.Data.Entity;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;

namespace Whoville.Data.Repositories
{
  public class CharacterRepository : ICharacterRepository
  {
    private WhovilleContext _db;

    public CharacterRepository(WhovilleContext db)
    {
      _db = db;
    }

    public Character Get(int id)
    {
      return _db.Characters.Single(x => x.Id == id);
    }

    public Character Save(Character entity)
    {
      if (entity.Story == null)
      {
        throw new ArgumentException("A Character requires a Story.");
      }
      
      if (entity.Id == 0)
      {
        //new entry
        _db.Characters.Add(entity);
      }
      else
      {
        //existing entry
        _db.Characters.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
      }

      _db.SaveChanges();

      return entity;
    }
  }
}
