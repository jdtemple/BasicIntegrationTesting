using System.Collections.Generic;
using Whoville.Data.Models;

namespace Whoville.Data.Interfaces
{
  public interface ICabinetRepository
  {
    Cabinet Get(int id);

    List<Cabinet> GetAll();

    Cabinet Save(Cabinet entity);
  }
}
