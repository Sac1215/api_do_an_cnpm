using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos
{
    public class AppUserInfoCmtDTO
    {

        required public string FullName { get; set; }
        required public string Email { get; set; }
        required public string PhoneNumber { get; set; }
    }
}