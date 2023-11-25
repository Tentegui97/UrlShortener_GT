using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortener_GT.Proyecto.Models;
using UrlShortener_GT.Proyecto.Services;


namespace UrlShortener_GT.Proyecto.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserService _userService;

        public AuthenticationController(IConfiguration config, UserService userService)
        {
            _config = config; 
            _userService = userService;

        }

        [HttpPost("authenticate")] 
        public IActionResult Autenticar(AuthenticationRequestBody authenticationRequestBody) 
        {
            
            var user = _userService.ValidateUser(authenticationRequestBody); 

            if (user is null) 
                return Unauthorized();

            
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"])); 

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); 
            claimsForToken.Add(new Claim("given_name", user.Username));

            var jwtSecurityToken = new JwtSecurityToken( 
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() 
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }
    }
}