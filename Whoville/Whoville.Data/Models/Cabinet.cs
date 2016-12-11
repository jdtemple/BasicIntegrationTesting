using System.Collections.Generic;
using Whoville.Data.Models.Base;

namespace Whoville.Data.Models
{
  public class Cabinet : Entity
  {
    public string Name { get; set; }

    public virtual ICollection<Folder> Folders { get; set; }
  }
}
