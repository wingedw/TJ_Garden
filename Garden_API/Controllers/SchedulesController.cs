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
    public class SchedulesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public SchedulesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedules()
        {
            return await _context.Schedules.Select(x => MapSchedulesDTO(x)).ToListAsync();
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchedulesDTO>> GetSchedules(int id)
        {
            var schedules = await _context.Schedules.FindAsync(id);

            if (schedules == null)
            {
                return NotFound();
            }

            return MapSchedulesDTO(schedules);
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedules(int id, SchedulesDTO schedulesdto)
        {
            if (id != schedulesdto.Schedule_Id)
            {
                return BadRequest();
            }

            var _schedule = await _context.Schedules.FindAsync(id);
            if (_schedule == null)
            {
                return NotFound();
            }

            _schedule.Schedule_Id = schedulesdto.Schedule_Id;
            _schedule.Plot_Id = schedulesdto.Plot_Id;
            _schedule.Event_Id = schedulesdto.Event_Id;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulesExists(id))
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

        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Schedules>> PostSchedules(SchedulesDTO schedulesdto)
        {
            var _newschedules = new Schedules
            {
                Schedule_Id = schedulesdto.Schedule_Id,
                Plot_Id = schedulesdto.Plot_Id,
                Event_Id = schedulesdto.Event_Id
            };
            _context.Schedules.Add(_newschedules);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedules", new { id = _newschedules.Schedule_Id }, _newschedules);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            var schedules = await _context.Schedules.FindAsync(id);
            if (schedules == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedules);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchedulesExists(int id)
        {
            return _context.Schedules.Any(e => e.Schedule_Id == id);
        }
        private static SchedulesDTO MapSchedulesDTO(Schedules schedules) => new()
        {
            Schedule_Id = schedules.Schedule_Id,
            Plot_Id = schedules.Plot_Id,
            Event_Id = schedules.Event_Id
        };

    }
}
