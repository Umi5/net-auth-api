using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_auth_api.Models
{
    public class UserDto
    {
        public required string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public required string Pasword { get; set; } = string.Empty;
    }
}