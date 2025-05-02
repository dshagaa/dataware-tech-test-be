using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveysApi.Interfaces;
using SurveysApi.Models;
using SurveysApi.Data;
using Microsoft.AspNetCore.Authorization;
using SurveysApi.Utilities;
using Microsoft.AspNetCore.Identity.Data;

namespace SurveysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DefaultDbContext _context;
        private readonly JWT _jwt;
        private readonly DateTimeUtils _dateTimeUtils;
        public AuthController(DefaultDbContext context, JWT jwt, DateTimeUtils dateTimeUtils)
        {
            _dateTimeUtils = dateTimeUtils;
            _jwt = jwt;
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] SignInRequest user)
        {
            var existingUser = await _context.User
                .Include(u => u.Role)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser == null)
            {
                return NotFound();
            }
            // Check if the user is active
            if (existingUser.Status.Code != "active")
            {
                return Unauthorized("user_not_active");
            }
            // Check if the password is correct
            if (_jwt.EncryptSHA256(user.Password) != existingUser.Password)
            {
                return Unauthorized("invalid_password");
            }

            // Generate a token for the user
            existingUser.Token = _jwt.GenerateJWTToken(existingUser);
            existingUser.UpdatedAt = _dateTimeUtils.Now();

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return StatusCode(StatusCodes.Status200OK, new
            {
                token = existingUser.Token,
                user = new
                {
                    id = existingUser.Id,
                    username = existingUser.Username,
                    firstName = existingUser.FirstName,
                    lastName = existingUser.LastName,
                    role = existingUser.Role.Code,
                    status = existingUser.Status.Code
                }
            });
        }

        // POST: api/auth/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] SignUpRequest user)
        {
            // Check if the username already exists
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser != null)
            {
                return BadRequest("username_already_taken");
            }

            Role userRole = await _context.Role
                .FirstAsync(r => r.Code == "user");
            Status userStatus = await _context.Status
                .FirstAsync(s => s.Code == "active");

            var newUser = new User();
            // Hash the password (this is just a placeholder, implement your own password hashing logic)
            newUser.Username = user.Username;
            newUser.Password = _jwt.EncryptSHA256(user.Password); // Replace with hashed password
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.StatusId = userStatus.Id;
            newUser.RoleId = userRole.Id;
            newUser.Role = userRole;
            newUser.Status = userStatus;

            // return Ok(newUser);
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, new
            {
                id = newUser.Id,
                username = newUser.Username,
                firstName = newUser.FirstName,
                lastName = newUser.LastName,
                role = userRole.Code,
                status = userStatus.Code
            });
        }

        // GET: api/auth/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromHeader] string Authorization)
        {
            var token = Authorization.Split(" ")[1];
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Token == token);

            if (existingUser == null)
            {
                return NotFound("user_not_found");
            }

            // Invalidate the token
            existingUser.Token = string.Empty;
            existingUser.UpdatedAt = _dateTimeUtils.Now();
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "User logged out successfully"
            });
        }

        // POST: api/auth/recovery-password
        [HttpPost("recovery-password")]
        public async Task<IActionResult> RecoveryPassword([FromBody] User user)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                message = "This endpoint is not implemented yet"
            });
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
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                message = "This endpoint is not implemented yet"
            });
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