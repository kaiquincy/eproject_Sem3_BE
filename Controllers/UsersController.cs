using Microsoft.AspNetCore.Mvc;
using CareerGuidancePlatform.Data;
using CareerGuidancePlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CareerGuidancePlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) => _context = context;

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
            => Ok(await _context.Users.AsNoTracking().ToListAsync());

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            // lưu password đã hash, validate… nếu cần
            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User updated)
        {
            if (id != updated.UserId)
                return BadRequest();

            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();

            // Chỉ cập nhật các field được phép
            u.FullName = updated.FullName;
            u.Email    = updated.Email;
            u.Career   = updated.Career;
            u.Niche    = updated.Niche;
            u.Role     = updated.Role;
            // (không đổi password ở đây)

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();
            _context.Users.Remove(u);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
