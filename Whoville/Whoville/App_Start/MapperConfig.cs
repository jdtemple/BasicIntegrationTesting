using AutoMapper;
using System;
using Whoville.Data.Models;
using Whoville.Data.ViewModels;

namespace Whoville.App_Start
{
  public static class MapperConfig
  {
    public static void RegisterMaps()
    {
      Mapper.Initialize(x =>
      {
        x.CreateMap<Folder, FolderModel>().PreserveReferences().ReverseMap().PreserveReferences();

        x.CreateMap<Cabinet, CabinetModel>().PreserveReferences().ReverseMap().PreserveReferences();

        x.CreateMap<FileModel, File>().PreserveReferences().ReverseMap().PreserveReferences();
      });
    }
  }
  
}
