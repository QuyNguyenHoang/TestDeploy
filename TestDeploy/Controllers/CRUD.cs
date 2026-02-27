using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDeploy.Controllers.Data;

namespace TestDeploy.Controllers
{
    [Route("api/test-datas")]
    [ApiController]
    public class TestDatasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= GET ALL =================
        [HttpGet("view")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.TestDatas.ToListAsync();
            return Ok(data);
        }

        // ================= GET BY ID =================
        [HttpGet("viewbyId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _context.TestDatas.FindAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // ================= CREATE =================
        [HttpPost("create")]
        public async Task<IActionResult> Create(TestData model)
        {
            _context.TestDatas.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // ================= UPDATE =================
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, TestData model)
        {
            var data = await _context.TestDatas.FindAsync(id);

            if (data == null)
                return NotFound();

            data.Name = model.Name;
            data.Description = model.Description;

            await _context.SaveChangesAsync();

            return Ok(data);
        }

        // ================= DELETE =================
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.TestDatas.FindAsync(id);

            if (data == null)
                return NotFound();

            _context.TestDatas.Remove(data);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}