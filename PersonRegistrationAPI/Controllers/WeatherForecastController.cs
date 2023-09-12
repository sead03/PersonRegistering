using Microsoft.AspNetCore.Mvc;
using PersonRegistrationAPI.ViewModels;

namespace PersonRegistrationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return "bjhbhhbj";
        }
        [HttpPost(Name = "postid")]
        public string create([FromBody] CreateViewModel model)
        {
            var asd = model.Id;

            return "sucess";
;        }
    }
}