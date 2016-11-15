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
        x.CreateMap<Character, CharacterModel>().PreserveReferences();
        x.CreateMap<CharacterModel, Character>().PreserveReferences();
        x.CreateMap<Story, StoryModel>().PreserveReferences();
        x.CreateMap<StoryModel, Story>().PreserveReferences();
      });
    }
  }
}
