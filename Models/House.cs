using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api_do_an_cnpm.Models.api_do_an_cnpm.Models;

namespace api_do_an_cnpm.Models
{
    public class House
    {
        // Mã ID của nhà
        public int Id { get; set; }

        // Tiêu đề của nhà (Tên hoặc loại nhà ở)
        public string Title { get; set; }

        // Địa chỉ của nhà
        public string Address { get; set; }

        // Mô tả về nhà ở
        public string Description { get; set; }

        // Giá thuê của nhà
        public double Price { get; set; }

        // Tên của khu nhà (Ví dụ: Ký túc xá sinh viên, Nhà ở sinh viên, v.v.)
        public string AccommodationName { get; set; }

        // Xếp hạng của nhà (Có thể là từ 1 đến 5 sao)
        public double Rating { get; set; }

        // Số điện thoại liên hệ với chủ nhà
        public string ContactPhone { get; set; }

        // Các phương tiện có thể chấp nhận (Ví dụ: xe đạp, xe máy, ô tô)
        public string AcceptableVehicles { get; set; }

        // Vị trí vĩ độ trên bản đồ
        public double Latitude { get; set; }

        // Vị trí kinh độ trên bản đồ
        public double Longitude { get; set; }

        // Thời gian đóng cửa của nhà (Giờ đóng cửa mỗi ngày)
        public string CloseTime { get; set; }
        public bool IsAccept { get; set; }
        public bool IsBlock { get; set; }
        public DateTime DateCreate { get; set; }
        // Liên kết với các tiện nghi của nhà (Một nhà có thể có nhiều tiện nghi)
        public ICollection<Facility>? Facilities { get; set; }

        // Liên kết với các bình luận của sinh viên hoặc khách thuê
        public ICollection<Comment>? Comments { get; set; }

        // Liên kết với chủ nhà (AppUser với vai trò OwnerHouse)
        public string OwnerId { get; set; }
        public AppUser? Owner { get; set; }
    }

}
