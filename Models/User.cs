using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_auth_api.Models
{
    public class User
    {
        public string Username { get; set; } = String.Empty;
        public string PaswordHash { get; set; } = String.Empty;
    }
}