using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles
{
    public class CommandsAutoMapperProfile: Profile
    {
        public CommandsAutoMapperProfile()
        {
            //Source -> target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            
        }
    }
}