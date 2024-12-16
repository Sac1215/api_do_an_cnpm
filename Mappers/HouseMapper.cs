using System;
using System.Collections.Generic;
using System.Linq;
using api_do_an_cnpm.Dtos;
using api_do_an_cnpm.Dtos.HouseDTO;
using api_do_an_cnpm.Models;
using api_do_an_cnpm.Models.api_do_an_cnpm.Models;
using AutoMapper;

namespace api_do_an_cnpm.Mappers
{
    public class HouseMapper : Profile
    {

        public HouseMapper()
        {

            CreateMap<House, HouseDTO>().ReverseMap();
            CreateMap<Facility, FacilityDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();

            CreateMap<Facility, FacilityDTO>().ReverseMap();


            CreateMap<Comment, CommentDetailDTO>()

               .ForMember(dest => dest.Author, opt => opt.MapFrom(src => (src.User ?? new AppUser() { FullName = "Unknown" }).FullName ?? ""))
               .ForMember(dest => dest.AvatarAuthor, opt => opt.MapFrom(src => "https://firebasestorage.googleapis.com/v0/b/tinhthanfoundation.appspot.com/o/Avatar%2Favatar-1.jpg?alt=media&token=42770a8c-6ac4-4aa6-9769-0f8d35fadcc9"))
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)) // Map User to AppUserInfoCmtDTO

               .ReverseMap();
            // ? Mapping ở đây khi khởi taọh House sẽ tự động tìm và khởi faojc facilities
            CreateMap<HouseDTO, House>()
                                 .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => src.Facilities));

        }
    }
}