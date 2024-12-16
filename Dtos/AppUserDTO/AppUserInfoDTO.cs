using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos
{
    public class AppUserInfoDTO
    {
        required public string Id { get; set; }
        required public string Roles { get; set; }
        required public string FullName { get; set; }
        required public string UserName { get; set; }
        required public string Address { get; set; }
        required public string Email { get; set; }
        required public string PhoneNumber { get; set; }


    }
}