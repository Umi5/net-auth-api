using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using net_auth_api.Models;

namespace net_auth_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request){
            string passwordHash = 
                BCrypt.Net.BCrypt.HashPassword(request.Pasword);

            user.Username = request.Username;
            user.PaswordHash = passwordHash;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request){
            
            if(user.Username != request.Username ) {
                return BadRequest("User not found");
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Pasword, user.PaswordHash)){
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);


            return Ok(token);
        }

        private string CreateToken(User user){
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}