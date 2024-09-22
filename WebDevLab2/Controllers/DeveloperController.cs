using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevLab2.Model.Context;
using WebDevLab2.Model;

namespace WebDevLab2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class DeveloperController: Controller
    {
        private readonly MainContext _context;
        public DeveloperController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
        {
            return await _context.Developer.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(int id)
        {
            var developer = await _context.Developer.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }
            return developer;

        }

        [HttpPost]
        public async Task<ActionResult<Developer>> PostDeveloper(Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Developer.Add(developer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeveloper", new { id = developer.Id }, developer);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloper(int id, Developer devrloper)
        {
            if (id != devrloper.Id)
            {
                return BadRequest();
            }
            _context.Entry(devrloper).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevrloper(int id)
        {
            var devrloper = await _context.Developer.FindAsync(id);
            if (devrloper == null)
            {
                return NotFound();
            }
            _context.Developer.Remove(devrloper);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool DeveloperExists(int id)
        {
            return _context.Developer.Any(e => e.Id == id);
        }

    }
}
