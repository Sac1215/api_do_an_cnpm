using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_do_an_cnpm.Dtos
{
    public class CommentDTO
    {
        // public int Id { get; set; }

        [Required(ErrorMessage = "HouseId is required")]
        public int? HouseId { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }

        // public string? UserId { get; set; } = "";
    }
}
// public ICollection<ReplyDTO> Replies { get; set; }