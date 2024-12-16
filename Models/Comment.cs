using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace api_do_an_cnpm.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public int? HouseId { get; set; }
        public House? House { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser? User { get; set; }
        public int? ParentCommentId { get; set; }
        [ForeignKey(nameof(ParentCommentId))]
        public Comment ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; }

    }
}