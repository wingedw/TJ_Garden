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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsers()
        {
            return await _context.Users.Select(x => MapUsersDTO(x)).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDTO>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return MapUsersDTO(users);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, UsersDTO users)
        {
            if (id != users.User_Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(UsersDTO usersdto)
        {
            var _newuser = new Users
            {
                User_Id = usersdto.User_Id,
                First_Name = usersdto.First_Name,
                Last_Name = usersdto.Last_Name,
                Email = usersdto.Email
            };
            _context.Users.Add(_newuser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = _newuser.User_Id }, _newuser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.User_Id == id);
        }

        private static UsersDTO MapUsersDTO(Users users) => new()
        {
            User_Id = users.User_Id,
            First_Name = users.First_Name,
            Last_Name = users.Last_Name,
            Email = users.Email
        };

    }
}
