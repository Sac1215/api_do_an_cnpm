using api_do_an_cnpm.Dtos;
using api_do_an_cnpm.Dtos.AppUserDTO;
using api_do_an_cnpm.Enums;
using api_do_an_cnpm.Interfaces;
using api_do_an_cnpm.Models;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api_do_an_cnpm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        // private readonly JwtHelper _jwtHelper;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AppUserController(ITokenService tokenService, UserManager<AppUser> userManager, IAppUserRepository appUserRepository, IMapper mapper, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailService = emailService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _appUserRepository.GetAllAsync();
            var userDTOs = _mapper.Map<IEnumerable<AppUserMajorDTO>>(data);
            return ResponseHelper.Success(this, userDTOs ?? []);
        }

        [HttpGet("GetAllFillterMajor")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _appUserRepository.GetAllAsync();
            var userDTOs = _mapper.Map<IEnumerable<AppUserMajorDTO>>(users);
            return ResponseHelper.Success(this, userDTOs ?? []);
        }


        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {

                var appUser = await _appUserRepository.GetByIdAsync(id);
                if (appUser == null)
                {
                    return ResponseHelper.Error(this, "Không tìm thấy người dùng", 404);
                }
                var appUserDto = _mapper.Map<AppUserInfoDTO>(appUser);
                return ResponseHelper.Success(this, appUserDto);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }


        [HttpPost("Student/register")]
        public async Task<IActionResult> StudentRegister([FromBody] AppUserCreateDTO appUserDto)
        {
            var checkModelState = ResponseHelper.CheckModelStateAndReturnError(this);
            if (checkModelState != null) //? Không hợp lệ
            {
                return checkModelState;
            }
            try
            {
                var existingAppUser = await _userManager.FindByNameAsync(appUserDto.UserName);
                if (existingAppUser != null)
                {
                    return ResponseHelper.Error(this, "Tên đăng nhập đã tồn tại", 400);
                }

                var appUser = _mapper.Map<AppUser>(appUserDto);
                appUser.PasswordHash = _userManager.PasswordHasher.HashPassword(appUser, appUserDto.Password);
                appUser.Role = "Student";
                var result = await _userManager.CreateAsync(appUser);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Student");
                    return ResponseHelper.Success<object>(this, null, "Đăng kí thành công", EnumActionApi.Post);
                }

                return ResponseHelper.Error(this, "Đã xảy ra lỗi khi đăng ký người dùng", 400);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, $"Đã xảy ra lỗi: {ex.Message}", 500);
            }
        }
        [HttpPost("OwnerHouse/register")]
        public async Task<IActionResult> OwnerHouseRegister([FromBody] AppUserCreateDTO appUserDto)
        {
            var checkModelState = ResponseHelper.CheckModelStateAndReturnError(this);
            if (checkModelState != null) //? Không hợp lệ
            {
                return checkModelState;
            }
            try
            {
                var existingAppUser = await _userManager.FindByNameAsync(appUserDto.UserName);
                if (existingAppUser != null)
                {
                    return ResponseHelper.Error(this, "Tên đăng nhập đã tồn tại", 400);
                }

                var appUser = _mapper.Map<AppUser>(appUserDto);
                appUser.PasswordHash = _userManager.PasswordHasher.HashPassword(appUser, appUserDto.Password);
                appUser.Role = "OwnerHouse";

                var result = await _userManager.CreateAsync(appUser);
                if (result.Succeeded)
                {
                    var EntryGetAppUser = await _userManager.FindByNameAsync(appUserDto.UserName);


                    await _userManager.AddToRoleAsync(EntryGetAppUser, "OwnerHouse");
                    return ResponseHelper.Success<object>(this, null, "Đăng kí thành công", EnumActionApi.Post);
                }

                return ResponseHelper.Error(this, "Đã xảy ra lỗi khi đăng ký người dùng", 400);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, $"Đã xảy ra lỗi: {ex.Message}", 500);
            }
        }



        [HttpPost("Admin/register")]
        public async Task<IActionResult> AdminRegister([FromBody] AppUserCreateDTO appUserDto)
        {
            var checkModelState = ResponseHelper.CheckModelStateAndReturnError(this);
            if (checkModelState != null) //? Không hợp lệ
            {
                return checkModelState;
            }
            try
            {
                var existingAppUser = await _userManager.FindByNameAsync(appUserDto.UserName);
                if (existingAppUser != null)
                {
                    return ResponseHelper.Error(this, "Tên đăng nhập đã tồn tại", 400);
                }

                var appUser = _mapper.Map<AppUser>(appUserDto);
                appUser.PasswordHash = _userManager.PasswordHasher.HashPassword(appUser, appUserDto.Password);
                appUser.Role = "Admin";

                var result = await _userManager.CreateAsync(appUser);
                if (result.Succeeded)
                {
                    var EntryGetAppUser = await _userManager.FindByNameAsync(appUserDto.UserName);


                    await _userManager.AddToRoleAsync(EntryGetAppUser, "Admin");
                    return ResponseHelper.Success<object>(this, null, "Đăng kí thành công", EnumActionApi.Post);
                }

                return ResponseHelper.Error(this, "Đã xảy ra lỗi khi đăng ký người dùng", 400);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, $"Đã xảy ra lỗi: {ex.Message}", 500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AppUserLoginDTO appUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var appUser = await _appUserRepository.GetByUserNameAsync(appUserDto.UserName);
                // var appUser = await _userManager.Users
                //           .Include(u => u.StudentMajors)
                //           .ThenInclude(sm => sm.Major)
                //           .FirstOrDefaultAsync(u => u.UserName == appUserDto.UserName);

                if (appUser == null || !await _userManager.CheckPasswordAsync(appUser, appUserDto.Password))
                {
                    return ResponseHelper.Error(this, "Tên đăng nhập hoặc mật khẩu không chính xác", 400);
                }
                var roles = await _userManager.GetRolesAsync(appUser);
                // var token = _tokenService.CreateToken(appUser);
                var token = await _tokenService.CreateToken(appUser);
                var appUserInfoDtos = _mapper.Map<AppUserInfoDTO>(appUser);
                appUserInfoDtos.Roles = string.Join(", ", roles);
                var data = new
                {
                    Token = token,
                    User = appUserInfoDtos
                };

                return ResponseHelper.Success(this, data, "Đăng nhập thành công", EnumActionApi.Post);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, $"Đã xảy ra lỗi: {ex.Message}", 500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] AppUserUpdateDTO appUserDto)
        {
            var checkModelState = ResponseHelper.CheckModelStateAndReturnError(this);
            if (checkModelState != null) //? Không hợp lệ
            {
                return checkModelState;
            }
            try
            {
                var appUser = await _appUserRepository.GetByIdAsync(id);
                if (appUser == null)
                {
                    return NotFound("Không tìm thấy người dùng");
                }

                _mapper.Map(appUserDto, appUser);


                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    return ResponseHelper.Success<object>(this, null, "Cập nhật thành công", EnumActionApi.Update);
                }

                return ResponseHelper.Error(this, "Đã xảy ra lỗi khi cập nhật người dùng", 400);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, $"Đã xảy ra lỗi: {ex.Message}", 500);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] AppUserChangePassworDTO changePasswordDto)
        {
            var checkModelState = ResponseHelper.CheckModelStateAndReturnError(this);
            if (checkModelState != null) //? Không hợp lệ
            {
                return checkModelState;
            }
            if (changePasswordDto.newPassword != changePasswordDto.confirmPassword)
            {
                return ResponseHelper.Error(this, "Mật khẩu mới và xác nhận mật khẩu không khớp", 400);
            }
            try
            {
                var appUser = await _userManager.FindByIdAsync(changePasswordDto.Id);
                if (appUser == null)
                {
                    return ResponseHelper.Error(this, "Không tìm thấy người dùng", 404);
                }

                var result = await _userManager.ChangePasswordAsync(appUser, changePasswordDto.oldPassword, changePasswordDto.newPassword);
                if (result.Succeeded)
                {

                    return ResponseHelper.Success<object>(this, null, "Đổi mật khẩu thành công", EnumActionApi.Update);
                }
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        // Console.WriteLine();
                        return ResponseHelper.Error(this, $"{error.Description}", 400);
                    }
                }

                return ResponseHelper.Error(this, $"Đã xảy ra lỗi khi đổi mật khẩu {string.Join(", ", result.Errors)}", 400);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, $"Đã xảy ra lỗi: {ex.Message}", 500);
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] AppUserForgotDTO appUserForgotDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(appUserForgotDTO.Email);
                if (user == null)
                {
                    return ResponseHelper.Error(this, "Không tìm thấy người dùng", 404);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                // var resetLink = Url.Action("ResetPassword", "AppUser", new { token, email = user.Email }, Request.Scheme);

                await _emailService.SendEmailAsync(user.Email, $"[YÊU CẦU] LẤY LẠI MẬT KHẨU", $"Vui lòng sao chép mã xác minh này sau đó quay trở lại ứng dụng và dán vào ô điền mã ở ứng dụng: {token}");

                return ResponseHelper.Success<object>(this, null, "Liên kết đặt lại mật khẩu đã được gửi đến email của bạn", EnumActionApi.Post);
            }
            catch (Exception ex)
            {

                return ResponseHelper.Error(this, $"Đã xảy ra lỗi vui lòng thử lại sau: {ex.Message}", 500);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] AppUserResetPasswordDTO appUserResetPasswordDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(appUserResetPasswordDTO.Email);
                if (user == null)
                {
                    return ResponseHelper.Error(this, "Không tìm thấy người dùng", 404);
                }

                var result = await _userManager.ResetPasswordAsync(user, appUserResetPasswordDTO.Token, appUserResetPasswordDTO.NewPassword);
                if (!result.Succeeded)
                {
                    return ResponseHelper.Error(this, $"Đã xảy ra lỗi khi đặt lại mật khẩu: {string.Join(", ", result.Errors.Select(e => e.Description))}", 400);
                }

                return ResponseHelper.Success<object>(this, null, "Mật khẩu đã được đặt lại thành công", EnumActionApi.Post);
            }
            catch (Exception ex)
            {

                return ResponseHelper.Error(this, $"Đã xảy ra lỗi vui lòng thử lại sau: {ex.Message}", 500);
            }
        }
    }


}