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
    public class GaneCommentaryController : Controller
    {
        private readonly MainContext _context;
        public GaneCommentaryController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameCommentary>>> GetGameCommentaries()
        {
            return await _context.GameCommentary.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameCommentary>> GetGameCommentary(int id)
        {
            var gameCommentary = await _context.GameCommentary.FindAsync(id);
            if (gameCommentary == null)
            {
                return NotFound();
            }
            return gameCommentary;

        }

        [HttpPost]
        public async Task<ActionResult<GameCommentary>> PostDeveloper(GameCommentary gameCommentary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.GameCommentary.Add(gameCommentary);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGameCommentary", new { id = gameCommentary.Id }, gameCommentary);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloper(int id, GameCommentary gameCommentary)
        {
            if (id != gameCommentary.Id)
            {
                return BadRequest();
            }
            _context.Entry(gameCommentary).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameCommentaryExists(id))
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
        public async Task<IActionResult> DeleteGameCommentary(int id)
        {
            var gameCommentary = await _context.GameCommentary.FindAsync(id);
            if (gameCommentary == null)
            {
                return NotFound();
            }
            _context.GameCommentary.Remove(gameCommentary);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool GameCommentaryExists(int id)
        {
            return _context.GameCommentary.Any(e => e.Id == id);
        }

    }
}
