using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDeploy.Data;
using TestDeploy.Models;

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

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.TestDatas.ToListAsync());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _context.TestDatas.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(TestData model)
        {
            _context.TestDatas.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TestData model)
        {
            var item = await _context.TestDatas.FindAsync(id);
            if (item == null) return NotFound();

            item.Name = model.Name;
            item.Description = model.Description;

            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.TestDatas.FindAsync(id);
            if (item == null) return NotFound();

            _context.TestDatas.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}