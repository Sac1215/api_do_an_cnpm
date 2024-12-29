using System.ComponentModel.DataAnnotations;

namespace api_do_an_cnpm.Dtos
{
    public class AppUserCreateDTO
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ và tên không được dài quá 100 ký tự")]
        required public string FullName { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được dài quá 50 ký tự")]
        required public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        required public string Password { get; set; }

         public string? Address { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]

        required public string Email { get; set; }
      
        public string? PhoneNumber { get; set; } 
    }
}