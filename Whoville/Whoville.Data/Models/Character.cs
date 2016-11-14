using Whoville.Data.Models.Base;

namespace Whoville.Data.Models
{
  public class Character : Entity
  {
    public string Name { get; set; }

    public int StoryId { get; set; }

    public virtual Story Story { get; set; }
  }
}
