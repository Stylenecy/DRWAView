using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_API.DTO
{
    public class LoginDTO
    {
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}