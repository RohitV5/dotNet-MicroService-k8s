using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public IPlatformRepo _repository { get; }
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController( IPlatformRepo repository, 
                                    IMapper mapper, 
                                    ICommandDataClient commandDataClient, 
                                    IMessageBusClient messageBusClient
                                )
        {
            _commandDataClient = commandDataClient;
            _mapper = mapper;
            _repository = repository;
            _messageBusClient = messageBusClient;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("Gettting platforms-->");

            var platformItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));

        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);

            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);

            //On create platform the id will be autoincremented and assigned to platformModel object.
            _repository.CreatePlatform(platformModel);


            _repository.SaveChanges();

            Console.WriteLine($"Saved platform========== {platformModel.Id}");

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            //Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not send synchronously {ex.Message}");
            }

            //Send Async Message
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
                platformPublishedDto.Event = "Platform Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            //CreatedAtRoute will send a header with Location as https://localhost:7068/api/Platforms/5
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }


    }
}