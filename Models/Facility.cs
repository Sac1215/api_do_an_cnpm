using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Models
{
    namespace api_do_an_cnpm.Models
    {
        public class Facility
        {
            public int Id { get; set; }
            public string Title { get; set; } // Tên tiện nghi
            public string Content { get; set; } // Nội dung chi tiết tiện nghi

            // Liên kết với House (Nhà)
            public int HouseId { get; set; }
            public House House { get; set; }
        }

    }
}