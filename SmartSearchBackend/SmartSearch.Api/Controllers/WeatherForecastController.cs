/*
using Microsoft.AspNetCore.Mvc;

namespace SmartSearch.Api.Controllers
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
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRabbitMQProducer rabbitMQProducer)
        {
            _logger = logger;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var queryData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            //отправляем данные запроса в очередь, потребитель получит эти данные из очереди
            _rabbitMQProducer.SendQueryMessage(queryData);
            return queryData;
        }
    }
}
*/