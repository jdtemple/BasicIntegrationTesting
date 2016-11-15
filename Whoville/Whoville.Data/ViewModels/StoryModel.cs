using System.Collections.Generic;
using Whoville.Data.ViewModels.Base;

namespace Whoville.Data.ViewModels
{
  public class StoryModel : EntityModel
  {
    public string Name { get; set; }

    public ICollection<CharacterModel> Characters { get; set; }
  }
}
