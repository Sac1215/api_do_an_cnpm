using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos.HouseDTO
{
    public class HouseDetailDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public double Rating { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ContactPhone is required")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "AcceptableVehicles is required")]
        public string AcceptableVehicles { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "CloseTime is required")]
        public string CloseTime { get; set; }

        // [Required(ErrorMessage = "IsAccept is required")]
        public bool IsAccept { get; set; } = false;

        // [Required(ErrorMessage = "IsBlock is required")]
        public bool IsBlock { get; set; } = false;

        [Required(ErrorMessage = "DateCreate is required")]
        public DateTime DateCreate { get; set; }

        [Required(ErrorMessage = "AccommodationName is required")]
        public string AccommodationName { get; set; }

        public ICollection<FacilityDTO>? Facilities { get; set; }
        public ICollection<CommentDetailDTO>? Comments { get; set; }


        [Required(ErrorMessage = "OwnerId is required")]
        public string OwnerId { get; set; }
    }
}