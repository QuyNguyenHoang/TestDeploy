using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDeploy.Controllers.Data;

namespace TestDeploy.Controllers
{
    [Route("api/Test/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CRUDController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= GET ALL =================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.TestDatas.ToListAsync();
            return Ok(data);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _context.TestDatas.FindAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // ================= CREATE =================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestData model)
        {
            await _context.TestDatas.AddAsync(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TestData model)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.TestDatas.FindAsync(id);

            if (data == null)
                return NotFound();

            _context.TestDatas.Remove(data);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }
    }
}