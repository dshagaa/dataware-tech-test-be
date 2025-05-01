using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveysApi.Models;
using SurveysApi.Data;

namespace SurveysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DefaultDbContext _context;
        public AuthController(DefaultDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser == null)
            {
                return Unauthorized();
            }

            // Verify the password (this is just a placeholder, implement your own password verification logic)
            if (!existingUser.Password.Equals(user.Password))
            {
                return Unauthorized();
            }

            // Generate a token for the user (this is just a placeholder, implement your own token generation logic)
            existingUser.Token = Guid.NewGuid().ToString();
            existingUser.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok(existingUser);
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            // Check if the username already exists
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser != null)
            {
                return Conflict("username_already_exists");
            }

            // Check if the password is strong enough (this is just a placeholder, implement your own password strength check)

            // Hash the password (this is just a placeholder, implement your own password hashing logic)
            user.Password = user.Password; // Replace with hashed password
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // GET: api/auth/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] User user)
        {
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser == null)
            {
                return Unauthorized();
            }

            // Invalidate the token (this is just a placeholder, implement your own token invalidation logic)
            existingUser.Token = string.Empty;
            existingUser.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Logged out successfully");
        }

        // POST: api/auth/recovery-password
        [HttpPost("recovery-password")]
        public async Task<IActionResult> RecoveryPassword([FromBody] User user)
        {
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser == null)
            {
                return NotFound("user_not_found");
            }

            // Generate a recovery token (this is just a placeholder, implement your own token generation logic)
            existingUser.RecoveryToken = Guid.NewGuid().ToString();
            existingUser.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingUser.RecoveryToken);
        }

        // GET: api/auth/change-password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] User user)
        {
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.RecoveryToken == user.RecoveryToken);

            if (existingUser == null)
            {
                return NotFound("recovery_token_not_found");
            }

            // Check if the new password is strong enough (this is just a placeholder, implement your own password strength check)
            
            // Check if the password is the same as the old password (this is just a placeholder, implement your own password comparison logic)
            
            // Hash the new password (this is just a placeholder, implement your own password hashing logic)
            existingUser.Password = user.Password; // Replace with hashed password
            existingUser.RecoveryToken = string.Empty;
            existingUser.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Password changed successfully");
        }
    }
}