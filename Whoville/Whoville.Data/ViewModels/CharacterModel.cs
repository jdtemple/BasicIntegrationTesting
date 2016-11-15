using Whoville.Data.ViewModels.Base;

namespace Whoville.Data.ViewModels
{
  public class CharacterModel : EntityModel
  {
    public string Name { get; set; }

    public int StoryId { get; set; }

    public StoryModel Story { get; set; }
  }
}
