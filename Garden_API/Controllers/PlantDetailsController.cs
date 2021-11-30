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
    public class PlantDetailsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PlantDetailsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/PlantDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDetailsDTO>>> GetPlants()
        {
            return await _context.Plants.Select(x => MapPlantDetailsDTO(x)).ToListAsync();
        }

        // GET: api/PlantDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDetailsDTO>> GetPlantDetails(int id)
        {
            var plantDetails = await _context.Plants.FindAsync(id);

            if (plantDetails == null)
            {
                return NotFound();
            }

            return MapPlantDetailsDTO(plantDetails);
        }

        // PUT: api/PlantDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantDetails(int id, PlantDetailsDTO plantdetailsdto)
        {
            if (id != plantdetailsdto.Plant_Id)
            {
                return BadRequest();
            }

            var _plantdetails = await _context.Plants.FindAsync(id);
            if (_plantdetails == null)
            {
                return NotFound();
            }

            _plantdetails.Plant_Id = plantdetailsdto.Plant_Id;
            _plantdetails.Plant_Type = plantdetailsdto.Plant_Type;
            _plantdetails.Common_Name = plantdetailsdto.Common_Name;
            _plantdetails.Scientific_Name = plantdetailsdto.Scientific_Name;
            _plantdetails.Sprout_Min = plantdetailsdto.Sprout_Min;
            _plantdetails.Sprout_Max = plantdetailsdto.Sprout_Max;
            _plantdetails.Spacing = plantdetailsdto.Spacing;
            _plantdetails.Description = plantdetailsdto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantDetailsExists(id))
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

        // POST: api/PlantDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlantDetails>> PostPlantDetails(PlantDetailsDTO plantdetailsdto)
        {
            var _newplantsdetails = new PlantDetails
            {
                Plant_Id = plantdetailsdto.Plant_Id,
                Plant_Type = plantdetailsdto.Plant_Type,
                Common_Name = plantdetailsdto.Common_Name,
                Scientific_Name = plantdetailsdto.Scientific_Name,
                Sprout_Min = plantdetailsdto.Sprout_Min,
                Sprout_Max = plantdetailsdto.Sprout_Max,
                Spacing = plantdetailsdto.Spacing,
                Description = plantdetailsdto.Description
            };
            _context.Plants.Add(_newplantsdetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantDetails", new { id = _newplantsdetails.Plant_Id }, _newplantsdetails);
        }

        // DELETE: api/PlantDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantDetails(int id)
        {
            var plantDetails = await _context.Plants.FindAsync(id);
            if (plantDetails == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plantDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantDetailsExists(int id)
        {
            return _context.Plants.Any(e => e.Plant_Id == id);
        }

        private static PlantDetailsDTO MapPlantDetailsDTO(PlantDetails plantdetails) => new()
        {
            Plant_Id = plantdetails.Plant_Id,
            Plant_Type = plantdetails.Plant_Type,
            Common_Name = plantdetails.Common_Name,
            Scientific_Name = plantdetails.Scientific_Name,
            Sprout_Min = plantdetails.Sprout_Min,
            Sprout_Max = plantdetails.Sprout_Max,
            Spacing = plantdetails.Spacing,
            Description = plantdetails.Description
        };
    }
}
