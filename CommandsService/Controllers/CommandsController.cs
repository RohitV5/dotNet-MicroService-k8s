using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepo _repository;
        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform");

            if (!_repository.PlatformExist(platformId))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = ("GetCommandForPlatform"))]
        public ActionResult<CommandCreateDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"Hit GetCommandForPlatform ${platformId} / ${commandId}");

            if (!_repository.PlatformExist(platformId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(platformId, commandId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));

        }


        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"Hit CreateCommandForPlatform Platform Id {platformId} / ${commandDto.CommandLine}");

            if (!_repository.PlatformExist(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _repository.CreateCommand(platformId, command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);  //will be having an id now

            return CreatedAtRoute(nameof(GetCommandForPlatform),
            new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);


        }
    }
}