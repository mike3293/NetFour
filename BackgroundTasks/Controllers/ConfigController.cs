using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundTasks.Controllers
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }


    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IOptionsMonitor<DynamicOptions> _optionsMonitor;

        public ConfigController(IOptionsMonitor<DynamicOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        [HttpGet]
        public DynamicOptions Get()
        {
            return _optionsMonitor.CurrentValue;
        }
    }
}
