using Whoville.Data.Models;

namespace Whoville.Data.Interfaces
{
  public interface ICharacterRepository
  {
    Character Get(int id);

    Character Save(Character entity);
  }
}
