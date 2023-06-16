using System.ComponentModel.DataAnnotations;

namespace WeatherForecastModule.Models;

public class ForecastOptions
{
    [Required]
    [Range(1, 10, ErrorMessage = "The {0} field must be at least {1} and no more than {2}")]
    public int ForecastCount { get; set; }
}