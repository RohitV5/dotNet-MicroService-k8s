using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {

        public PlatformsController()
        {
            
        }

        [HttpPost]
        public ActionResult Test()
        {
            Console.WriteLine("-----> Inbound POST # Commaand Service");

            return Ok("Inbound test of from PLatfroms Controller");
        }
    }
}