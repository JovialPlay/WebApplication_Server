using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebDevLab2.Model;
using WebDevLab2.Model.Context;
using Microsoft.AspNetCore.Authorization;

namespace WebDevLab2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly MainContext _context;
        public GameController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Game.Include(g => g.Comments).Include(g => g.Developer).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return game;

        }

        [HttpGet("{developerName:alpha}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetDevelopersGames(string developerName)
        {
            Developer dev = _context.Developer.Where(dev => dev.Login == developerName).First();
            return await _context.Game.Include(g => g.Comments).Include(g => g.Developer).Where(game => game.Developer.CompanyName == dev.CompanyName).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            game.Developer = _context.Developer.Where(dev => dev.Login == game.Developer.Login).First();

            _context.Game.Add(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGame", new { id = game.Id }, game);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }
            game.Developer = _context.Developer.Where(dev => dev.Login == game.Developer.Login).First();

            _context.Entry(game).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return game;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
