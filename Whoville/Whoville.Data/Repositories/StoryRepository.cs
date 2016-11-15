using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;

namespace Whoville.Data.Repositories
{
  public class StoryRepository : IStoryRepository
  {
    private WhovilleContext _db;

    public StoryRepository(WhovilleContext db)
    {
      _db = db;
    }

    public Story Get(int id)
    {
      return _db.Stories
        .Include(x => x.Characters)
        .Single(x => x.Id == id);
    }

    public List<Story> GetAll()
    {
      return _db.Stories
        .Include(x => x.Characters)
        .AsNoTracking()
        .ToList();
    }

    public Story Save(Story entity)
    {
      if (entity.Id == 0)
      {
        //new entry
        _db.Stories.Add(entity);
      }
      else
      {
        //existing entry
        _db.Stories.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
      }

      _db.SaveChanges();

      return entity;
    }
  }
}
