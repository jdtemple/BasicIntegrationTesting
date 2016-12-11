using Whoville.Data.ViewModels.Base;

namespace Whoville.Data.ViewModels
{
  public class FolderModel : EntityModel
  {
    public string Name { get; set; }

    public int CabinetId { get; set; }

    public CabinetModel Cabinet { get; set; }

    public String Extension { get; set; }

    public DateTime DeletedDate { get; set; }

    public DateTime CreationDate { get; set; }

    public int Pages { get; set; }
  }
}
