using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos.AppUserDTO
{
    public class AppUserUpdateDTO
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ và tên không được dài quá 100 ký tự")]
        required public string FullName { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        required public string Address { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        required public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        required public string PhoneNumber { get; set; }
    }
}