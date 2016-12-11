using System.Collections.Generic;
using Whoville.Data.ViewModels.Base;

namespace Whoville.Data.ViewModels
{
  public class CabinetModel : EntityModel
  {
    public string Name { get; set; }

    public ICollection<FolderModel> Folders { get; set; }
  }
}
