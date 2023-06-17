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
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("Getting Platforms from CommandsService");

            var platformsItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformsItems));
        }

        [HttpPost]
        public ActionResult Test()
        {
            Console.WriteLine("-----> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
}