using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos.AppUserDTO
{
    public class AppUserChangePassworDTO
    {
        required public string Id { get; set; }
        [Required(ErrorMessage = "Mật khẩu cũ là bắt buộc")]
        required public string oldPassword { get; set; }
        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
        required public string newPassword { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        required public string confirmPassword { get; set; }
    }
}