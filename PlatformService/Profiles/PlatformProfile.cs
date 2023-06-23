using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    //Derived from Automapper profile
    public class PlatformProfile:Profile
    {
        public PlatformProfile()
        {
            // Source->Target
            CreateMap<Platform,PlatformReadDto>();
            CreateMap<PlatformCreateDto,Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}