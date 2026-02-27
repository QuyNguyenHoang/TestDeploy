using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDeploy.Controllers.Data;

namespace TestDeploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDatasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/testdatas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.TestDatas.ToListAsync();
            return Ok(data);
        }

        // GET: api/testdatas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _context.TestDatas.FindAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        // POST: api/testdatas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestData model)
        {
            _context.TestDatas.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/testdatas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TestData model)
        {
            var data = await _context.TestDatas.FindAsync(id);
            if (data == null) return NotFound();

            data.Name = model.Name;
            data.Description = model.Description;

            await _context.SaveChangesAsync();
            return Ok(data);
        }

        // DELETE: api/testdatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.TestDatas.FindAsync(id);
            if (data == null) return NotFound();

            _context.TestDatas.Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}