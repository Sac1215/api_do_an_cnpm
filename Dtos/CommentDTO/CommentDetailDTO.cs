using System.ComponentModel.DataAnnotations;


namespace api_do_an_cnpm.Dtos
{
    public class CommentDetailDTO
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string AvatarAuthor { get; set; }
        public int? ParentCommentId { get; set; }
        public List<CommentDetailDTO> Replies { get; set; } = [];
        public int TotalReplies { get; set; }
        public string? UserId { get; set; }

        public AppUserInfoCmtDTO User { get; set; }
    }
}