using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos.AppUserDTO
{
    public class AppUserLoginDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        required public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        required public string Password { get; set; }
    }
}