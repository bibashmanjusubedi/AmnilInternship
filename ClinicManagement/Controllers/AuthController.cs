using ClinicManagement.DTOs.AuthRequests;
using ClinicManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace ClinicManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Assign default role Receptionist
            await _userManager.AddToRoleAsync(user, "Receptionist");

            return Ok(new
            {
                message = "User registered successfully!",
                user = new
                {
                    user.Id,
                    user.Email,
                    user.FullName
                }
            });



        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, dto.Password)))
            {
                return Unauthorized("Invalid login attempt.");
            }

            // 1. Get user roles
            var roles = await _userManager.GetRolesAsync(user);

            // 2. Create claims
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add role claims
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 3. JWT settings
            var jwtSettings = _configuration.GetSection("Jwt");
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // 4. Return token and basic info
            return Ok(new
            {
                message = "User logged in successfully!",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                user = new { user.Id, user.Email, user.FullName, Roles = roles }
            });
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found");

            // Optional: Check if the role is valid
            var validRoles = new[] { "Admin", "Doctor", "Receptionist" };
            if (!validRoles.Contains(dto.Role))
                return BadRequest("Invalid role");

            // Remove existing roles if needed (optional best practice)
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            // Add new role
            var result = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok($"Role '{dto.Role}' assigned to user '{user.Email}'");
        }



    }
}
