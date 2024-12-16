using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


using api_do_an_cnpm.Models;
namespace api_do_an_cnpm.Dtos.HouseDTO
{
    public class HouseDTO
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
        public bool IsAccept { get; set; }
        public bool IsBlock { get; set; }

        [Required(ErrorMessage = "AccommodationName is required")]
        public string AccommodationName { get; set; }

        public ICollection<FacilityDTO>? Facilities { get; set; }

        [Required(ErrorMessage = "OwnerId is required")]
        public string OwnerId { get; set; }
        public AppUserInfoCmtDTO? Owner { get; set; }
    }
}