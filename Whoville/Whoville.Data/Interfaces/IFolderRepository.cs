using Whoville.Data.Models;

namespace Whoville.Data.Interfaces
{
  public interface IFolderRepository
  {
    Folder Get(int id);

    Folder Save(Folder entity);
  }
}
