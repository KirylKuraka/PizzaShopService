using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetValues"), Authorize(Roles = "Customer")]
        public IEnumerable<string> GetValues()
        {
            _logger.LogInfo("info");
            _logger.LogDebug("debug");
            _logger.LogWarn("warn");
            _logger.LogError("error");

            return new string[] { "value1", "value2" };
        }
    }
}
