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
    public class DeveloperCommentaryController : Controller
    {
        private readonly MainContext _context;
        public DeveloperCommentaryController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperCommentary>>> GetDeveloperCommentaries()
        {
            return await _context.DeveloperCommentary.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperCommentary>> GetDeveloperCommentary(int id)
        {
            var developerCommentary = await _context.DeveloperCommentary.FindAsync(id);
            if (developerCommentary == null)
            {
                return NotFound();
            }
            return developerCommentary;

        }

        [HttpPost]
        public async Task<ActionResult<DeveloperCommentary>> PostDeveloperCommentary(DeveloperCommentary developerCommentary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.DeveloperCommentary.Add(developerCommentary);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeveloperCommentary", new { id = developerCommentary.Id }, developerCommentary);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloperCommentary(int id, DeveloperCommentary developerCommentary)
        {
            if (id != developerCommentary.Id)
            {
                return BadRequest();
            }
            _context.Entry(developerCommentary).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperCommentaryExists(id))
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
        public async Task<IActionResult> DeleteDeveloperCommentary(int id)
        {
            var developerCommentary = await _context.DeveloperCommentary.FindAsync(id);
            if (developerCommentary == null)
            {
                return NotFound();
            }
            _context.DeveloperCommentary.Remove(developerCommentary);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool DeveloperCommentaryExists(int id)
        {
            return _context.DeveloperCommentary.Any(e => e.Id == id);
        }

    }
}
