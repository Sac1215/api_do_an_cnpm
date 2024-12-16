using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos.AppUserDTO
{
    public class AppUserMajorDTO
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }


        public string? Role { get; set; }
        public string? Roles { get; set; }

    }
}