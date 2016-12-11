using AutoMapper;
using Whoville.Data.Models;
using Whoville.Data.ViewModels;

namespace Whoville.App_Start
{
  public static class MapperConfig
  {
    public static void RegisterMaps()
    {
      Mapper.Initialize(x => {
        x.CreateMap<Folder, FolderModel>().PreserveReferences();
        x.CreateMap<FolderModel, Folder>().PreserveReferences();
        x.CreateMap<Cabinet, CabinetModel>().PreserveReferences();
        x.CreateMap<CabinetModel, Cabinet>().PreserveReferences();
      });
    }
  }
}
