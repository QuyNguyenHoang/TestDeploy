using Microsoft.AspNetCore.Mvc;

namespace TestDeploy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<WeatherForecast> _data = new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 25,
                Summary = "Warm"
            },
            new WeatherForecast
            {
                Id = 2,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = 18,
                Summary = "Cool"
            }
        };

        // GET: api/WeatherForecast
        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> GetAll()
        {
            return Ok(_data);
        }

        // GET: api/WeatherForecast/1
        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> GetById(int id)
        {
            var item = _data.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/WeatherForecast
        [HttpPost]
        public ActionResult<WeatherForecast> Create(WeatherForecast model)
        {
            model.Id = _data.Max(x => x.Id) + 1;
            _data.Add(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/WeatherForecast/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, WeatherForecast model)
        {
            var item = _data.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            item.Date = model.Date;
            item.TemperatureC = model.TemperatureC;
            item.Summary = model.Summary;

            return NoContent();
        }

        // DELETE: api/WeatherForecast/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _data.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            _data.Remove(item);
            return NoContent();
        }
    }

    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}