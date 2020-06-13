using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;

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
        public ActionResult<Launch> Add(Launch launch)
        {
            var result = _launchService.Add(launch);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Launch>> Get()
        {
            var result = _launchService.Get();

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet("{launchId}")]
        public ActionResult<IEnumerable<Launch>> Get(Guid launchId)
        {
            var result = _launchService.Single(launchId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpPut]
        public ActionResult<Launch> Update(Launch launch)
        {
            var result = _launchService.UpdateDetachedEntity(launch, launch.LaunchId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpDelete("{launchId}")]
        public ActionResult<IEnumerable<Launch>> Delete(Guid launchId)
        {
            var result = _launchService.Delete(launchId);

            return result.Success ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
