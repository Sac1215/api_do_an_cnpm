using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos.AppUserDTO
{
    public class AppUserForgotDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập email")]
        required public string Email { get; set; }

    }
}