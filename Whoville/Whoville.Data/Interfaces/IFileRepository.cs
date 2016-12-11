using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whoville.Data.Models;

namespace Whoville.Data.Interfaces
{
  public interface IFileRepository
  {
    File Get(int id);

    File Save(File entity);
  }
}
