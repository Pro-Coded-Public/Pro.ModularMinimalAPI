///
/// One option for organising controllers
///
///
/// 


// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Http.HttpResults;
//
// namespace WeatherForecastModule.Controllers;
//
// // [ApiController]
// // [Route("[controller]")]
// public class WeatherForecastController : ControllerBase
// {
//
//     private readonly ILogger<WeatherForecastController> _logger;
//     private readonly IWeatherForecastService _weatherForecastService;
//
//     public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
//     {
//         _logger = logger;
//         _weatherForecastService = weatherForecastService;
//     }
//
//     /// <summary>
//     /// Returns IEnumerable<WeatherForecast>
//     /// </summary>
//     [HttpGet(Name = "GetWeatherForecast")]
//     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeatherForecast>))]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     [Produces("application/json")]
//     public async Task<Results<BadRequest, Ok<IEnumerable<WeatherForecast>>>> WeatherForecasts()
//     {
//         var results = _weatherForecastService.GetWeatherForecasts();
//         if (results is not null) 
//             return TypedResults.Ok(await _weatherForecastService.GetWeatherForecasts());
//         
//         _logger.LogInformation("No results found"); 
//         return TypedResults.BadRequest();
//     }
// }

