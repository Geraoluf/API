using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]                         
    [Route("api/[controller]")]               
    public class WeatherApiController : ControllerBase
    {
        [HttpGet("temperature")]            
        public IActionResult GetTemperature()
        {
           
            var temperature = new
            {
                Station = "06149",
                Value = 17.3,
                Unit = "°C"
            };

            return Ok(temperature);         
        }
    }
}
