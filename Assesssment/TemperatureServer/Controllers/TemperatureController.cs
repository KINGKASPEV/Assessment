using Microsoft.AspNetCore.Mvc;
using TemperatureServer.Models;

namespace TemperatureServer.Controllers
{
    [Route("api/temperature")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private static List<TemperatureData> temperatureDataList = new List<TemperatureData>();

        [HttpPost]
        public IActionResult Post([FromBody] TemperatureData data)
        {
            if (data == null)
            {
                return BadRequest("Temperature data is null.");
            }

            if (data.Temperature < -50 || data.Temperature > 150)
            {
                return BadRequest("Temperature value is out of valid range.");
            }

            data.Id = temperatureDataList.Count + 1;
            data.Timestamp = DateTime.Now;
            temperatureDataList.Add(data);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return temperatureDataList.Count == 0
            ? NotFound("No temperature data found.")
            : Ok(temperatureDataList);
        }

    }
}
