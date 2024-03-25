using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_auth_api.Models;

namespace net_auth_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public static User user = new User();

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

            

            return Ok(user);
        }

    }
}