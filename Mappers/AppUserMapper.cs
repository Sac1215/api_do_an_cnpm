using AutoMapper;
using api_do_an_cnpm.Models;
using api_do_an_cnpm.Dtos;
using api_do_an_cnpm.Dtos.AppUserDTO;

namespace api_do_an_cnpm.Mappers
{
    public class AppUserMapper : Profile
    {
        public AppUserMapper()
        {
            CreateMap<AppUser, AppUserCreateDTO>().ReverseMap();
            // CreateMap<AppUser, AppUserInfoDTO>().ReverseMap();
            CreateMap<AppUser, AppUserUpdateDTO>().ReverseMap();

            CreateMap<AppUser, AppUserInfoDTO>()

               .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => string.Join(", ", src.Roles))).ReverseMap();



            CreateMap<AppUser, AppUserInfoCmtDTO>().ReverseMap();
            // Mapping for AppUser to AppUserDTO
            CreateMap<AppUser, AppUserMajorDTO>()

                .ReverseMap();
        }

    }

}