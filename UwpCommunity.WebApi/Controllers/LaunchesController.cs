using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using UwpCommunity.WebApi.Models;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class LaunchesController : ControllerBase
    {
        private readonly ILogger<LaunchesController> _logger;
        private readonly ILaunchService _launchService;

        public LaunchesController(ILogger<LaunchesController> logger, ILaunchService launchService)
        {
            _logger = logger;
            _launchService = launchService;
        }

        [HttpPost]
        public ActionResult<LaunchDto> Add(Launch launch)
        {
            var result = _launchService.Add(launch);

            return result.Success ? Ok(new LaunchDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<LaunchDto>> Get()
        {
            var result = _launchService.Get();

            if (result.Success)
            {
                List<LaunchDto> launches = new List<LaunchDto>();
                foreach (var launch in result.Value)
                {
                    launches.Add(new LaunchDto(launch));
                }
                return Ok(launches);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{launchId}")]
        public ActionResult<LaunchDto> Get(Guid launchId)
        {
            var result = _launchService.Single(launchId);

            return result.Success ? Ok(new LaunchDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpPut]
        public ActionResult<LaunchDto> Update(Launch launch)
        {
            var result = _launchService.UpdateDetachedEntity(launch, launch.LaunchId);

            return result.Success ? Ok(new LaunchDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpDelete("{launchId}")]
        public ActionResult Delete(Guid launchId)
        {
            var result = _launchService.Delete(launchId);

            return result.Success ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
