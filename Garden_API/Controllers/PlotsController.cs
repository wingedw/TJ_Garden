using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garden_API.DAL;
using Garden_API.Models;

namespace Garden_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PlotsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Plots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plots>>> GetPlots()
        {
            return await _context.Plots.ToListAsync();
        }

        // GET: api/Plots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plots>> GetPlots(int id)
        {
            var plots = await _context.Plots.FindAsync(id);

            if (plots == null)
            {
                return NotFound();
            }

            return plots;
        }

        // PUT: api/Plots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlots(int id, Plots plots)
        {
            if (id != plots.Plot_Id)
            {
                return BadRequest();
            }

            _context.Entry(plots).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlotsExists(id))
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

        // POST: api/Plots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plots>> PostPlots(Plots plots)
        {
            _context.Plots.Add(plots);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlots", new { id = plots.Plot_Id }, plots);
        }

        // DELETE: api/Plots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlots(int id)
        {
            var plots = await _context.Plots.FindAsync(id);
            if (plots == null)
            {
                return NotFound();
            }

            _context.Plots.Remove(plots);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlotsExists(int id)
        {
            return _context.Plots.Any(e => e.Plot_Id == id);
        }
    }
}
