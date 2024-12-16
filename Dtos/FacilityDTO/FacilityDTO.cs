using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace api_do_an_cnpm.Dtos
{
    public class FacilityDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } // Tên tiện nghi
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } // Nội dung chi tiết tiện nghi
        public int HouseId { get; set; }
    }
}