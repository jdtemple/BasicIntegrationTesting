using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Whoville.Data.Interfaces;
using Whoville.Data.Models;

namespace Whoville.Data.Repositories
{
  public class StoryRepsitory : IStoryRepository
  {
    public List<Story> GetAll()
    {
      using (var db = new WhovilleContext())
      {
        return db.Stories
          .Include(x => x.Characters)
          .ToList();
      }
    }

    public Story Save(Story entity)
    {
      using (var db = new WhovilleContext())
      {
        if (entity.Id == 0)
        {
          //new entry
          db.Stories.Add(entity);
        }
        else
        {
          //existing entry
          db.Stories.Attach(entity);
          db.Entry(entity).State = EntityState.Modified;
        }

        db.SaveChanges();
      }

      return entity;
    }
  }
}
