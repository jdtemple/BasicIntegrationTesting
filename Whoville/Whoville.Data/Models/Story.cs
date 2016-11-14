using System.Collections.Generic;
using Whoville.Data.Models.Base;

namespace Whoville.Data.Models
{
  public class Story : Entity
  {
    public string Name { get; set; }

    public virtual ICollection<Character> Characters { get; set; }
  }
}
