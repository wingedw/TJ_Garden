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
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EventsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventsDTO>>> GetEvents()
        {
            return await _context.Events.Select(x=> MapEventsDTO(x)).ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventsDTO>> GetEvents(int id)
        {
            var events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }

            return MapEventsDTO(events);
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvents(int id, EventsDTO eventsdto)
        {
            if (id != eventsdto.Event_Id)
            {
                return BadRequest();
            }

            var _event = await _context.Events.FindAsync(id);
            if (_event ==  null)
            {
                return NotFound();
            }

            _event.Event_Id = eventsdto.Event_Id;
            _event.Name = eventsdto.Name;
            _event.Description = eventsdto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventsDTO>> CreateEvents(EventsDTO eventsdto)
        {
            var _newevent = new Events
            {
                Event_Id = eventsdto.Event_Id,
                Name = eventsdto.Name,
                Description = eventsdto.Description
            };
            _context.Events.Add(_newevent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvents", new { id = _newevent.Event_Id }, _newevent);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvents(int id)
        {
            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Event_Id == id);
        }

        private static EventsDTO MapEventsDTO(Events events) => new()
        {
            Event_Id = events.Event_Id,
            Name = events.Name,
            Description = events.Description
        };
    }
}
