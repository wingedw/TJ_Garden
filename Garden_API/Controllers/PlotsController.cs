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
        public async Task<ActionResult<IEnumerable<PlotsDTO>>> GetPlots()
        {
            return await _context.Plots.Select(x => MapPlotsDTO(x)).ToListAsync();
        }

        // GET: api/Plots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlotsDTO>> GetPlots(int id)
        {
            var plots = await _context.Plots.FindAsync(id);

            if (plots == null)
            {
                return NotFound();
            }

            return MapPlotsDTO(plots);
        }

        // PUT: api/Plots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlots(int id, PlotsDTO plotsdto)
        {
            if (id != plotsdto.Plot_Id)
            {
                return BadRequest();
            }

            var _plot = await _context.Plots.FindAsync(id);
            if (_plot == null)
            {
                return NotFound();
            }

            _plot.Plot_Id = plotsdto.Plot_Id;
            _plot.Plant_Id = plotsdto.Plot_Id;
            _plot.X_Location = plotsdto.X_Location;
            _plot.Y_Location = plotsdto.Y_Location;

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
        public async Task<ActionResult<Plots>> PostPlots(PlotsDTO plotsdto)
        {
            var _newplot = new Plots
            {
                Plot_Id = plotsdto.Plot_Id,
                Plant_Id = plotsdto.Plot_Id,
                X_Location = plotsdto.X_Location,
                Y_Location = plotsdto.Y_Location
            };
            _context.Plots.Add(_newplot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlots", new { id = _newplot.Plot_Id }, _newplot);
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
        private static PlotsDTO MapPlotsDTO(Plots plots) => new()
        {
            Plot_Id = plots.Plot_Id,
            Plant_Id = plots.Plot_Id,
            X_Location = plots.X_Location,
            Y_Location = plots.Y_Location
        };


    }
}
