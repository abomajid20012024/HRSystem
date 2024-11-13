namespace HRSystem.WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="AuthenticationController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Defines the _configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="loginModel">The loginModel<see cref="LoginModel"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Validate user and retrieve role
            var userRole = GetRoleForValidUser(loginModel);
            if (!string.IsNullOrEmpty(userRole))
            {
                var token = GenerateJwtToken(loginModel.Username, userRole);
                return Ok(new { token });
            }
            return Unauthorized("Invalid credentials");
        }

        /// <summary>
        /// The GetRoleForValidUser
        /// </summary>
        /// <param name="loginModel">The loginModel<see cref="LoginModel"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetRoleForValidUser(LoginModel loginModel)
        {
            if (loginModel.Username == "admin" && loginModel.Password == "123")
            {
                return "SuperAdmin";
            }
            else if (loginModel.Username == "user" && loginModel.Password == "123")
            {
                return "User";
            }
            return string.Empty;
        }

        /// <summary>
        /// The GenerateJwtToken
        /// </summary>
        /// <param name="username">The username<see cref="string"/></param>
        /// <param name="role">The role<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GenerateJwtToken(string username, string role)
        {
            var jwtSettings = _configuration.GetSection("Authentication");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(48),
                signingCredentials: credentials

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    /// <summary>
    /// Defines the <see cref="LoginModel" />
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }
    }
}
