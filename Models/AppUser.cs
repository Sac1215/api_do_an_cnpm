using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace api_do_an_cnpm.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FullName { get; set; }
        public string Address { get; set; }

        public string Role { get; set; }

        [JsonIgnore]


        public ICollection<Comment> Comments { get; set; }

        public ICollection<IdentityRole> Roles { get; set; }

        public ICollection<House> OwnedHouses { get; set; }




    }

}