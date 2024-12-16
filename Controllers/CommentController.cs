using api_do_an_cnpm.data;
using api_do_an_cnpm.Dtos;
using api_do_an_cnpm.Dtos.HouseDTO;
using api_do_an_cnpm.Enums;
using api_do_an_cnpm.Interfaces;
using api_do_an_cnpm.Models;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ResponseHelper;

namespace api_do_an_cnpm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(IMapper mapper, ICommentRepository commentRepository, ITokenService tokenService, IEmailService emailService, ApplicationDBContext context) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEmailService _emailService = emailService;
        private readonly ApplicationDBContext _context = context;

        // GET: api/Comments
        [HttpPost("{houseId}")]
        public async Task<IActionResult> GetComments(int houseId, PageRequest request)
        {
            var data = _commentRepository.GetAll()

                .Where(q => q.HouseId == houseId)
                .Include(q => q.User)
                .OrderBy(q => q.CreatedAt);

            var totalItems = data.Count();
            var paginatedComments = await _mapper.ProjectTo<CommentDetailDTO>(data
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize))
                .ToListAsync();

            var commentDTOs = new List<CommentDetailDTO>();

            foreach (var comment in paginatedComments)
            {
                var commentDto = _mapper.Map<CommentDetailDTO>(comment);

                var replyData = await _mapper.ProjectTo<CommentDetailDTO>(_commentRepository.GetAll()
                    .Where(reply => reply.ParentCommentId == comment.Id).Include(q => q.User)
                    .OrderBy(reply => reply.CreatedAt)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)).ToListAsync();

                var totalReplies = _commentRepository.GetAll()
                    .Where(reply => reply.ParentCommentId == comment.Id)
                    .Count();

                commentDto.Replies = replyData;
                commentDto.TotalReplies = totalReplies;

                commentDTOs.Add(commentDto);
            }

            return PaginatedSuccess(this, commentDTOs ?? [], request.PageNumber, request.PageSize, totalItems);
        }

        [HttpPost("GetReplies/{commentId}")]
        public async Task<IActionResult> GetReplies(int commentId, PageRequest request)
        {
            var data = _commentRepository.GetAll()
                .Where(c => c.ParentCommentId == commentId).Include(q => q.User);
            var totalItems = await data.CountAsync();
            var paginatedData = await _mapper.ProjectTo<CommentDetailDTO>(data
                .OrderBy(q => q.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize))
                .ToListAsync();

            return PaginatedSuccess(this, paginatedData, request.PageNumber, request.PageSize, totalItems);
        }

        // POST: api/Comments
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCommentOwerhouse([FromBody] CommentDTO commentDTO)
        {
            try
            {
                var userId = _tokenService.GetUserId();

                if (string.IsNullOrEmpty(userId)) return Error(this, "Không tìm thấy user", 0);
                var comment = _mapper.Map<Comment>(commentDTO);
                comment.UserId = userId;

                if (comment.ParentCommentId.HasValue)
                {
                    var parentComment = await _commentRepository.GetAll()
                        .Include(c => c.User)
                        .FirstOrDefaultAsync(c => c.Id == comment.ParentCommentId.Value);

                    if (parentComment == null)
                    {
                        return Error(this, "Không tìm thấy comment cha để reply", 404);
                    }

                    await _emailService.SendEmailAsync(parentComment.User.Email, "Chủ trọ đã phản hội bình luận của bạn", "Nội dung trả lời: " + comment.Content);

                }
                else
                {
                    if (comment.HouseId <= 0)
                    {
                        return Error(this, "HouseId không hợp lệ", 0);
                    }
                }
                await _commentRepository.AddAsync(comment);
                await _commentRepository.SaveAllAsync();
                return Success<object>(this, comment, "Bình luận thành công", EnumActionApi.Post);
            }
            catch (Exception ex)
            {
                return Error(this, "Bình luận không thành công", 404);
            }
        }
        [HttpPost("UserComment")]
        [Authorize]
        public async Task<IActionResult> PostCommentUser([FromBody] CommentDTO commentDTO)
        {
            try
            {
                var userId = _tokenService.GetUserId();

                if (string.IsNullOrEmpty(userId)) return Error(this, "Không tìm thấy user", 0);
                var comment = _mapper.Map<Comment>(commentDTO);
                comment.UserId = userId;

                if (comment.HouseId <= 0)
                {
                    return Error(this, "HouseId không hợp lệ", 0);
                }

                var ownerEmail = await _context.Houses
            .Where(h => h.Id == comment.HouseId)
            .Select(h => h.Owner.Email)
            .FirstOrDefaultAsync();





                // Ensure house.Owner.Email is not null
                if (string.IsNullOrEmpty(ownerEmail))
                {
                    return Error(this, "Không tìm thấy email của chủ nhà", 404);
                }

                // Send email to the house owner

                await _emailService.SendEmailAsync(ownerEmail, "UEF SERVICE Có bình luận mới", "Có khách hàng đã bình luận về phòng trọ của bạn. Vui lòng truy cập vào trang quản lý");

                await _commentRepository.AddAsync(comment);
                await _commentRepository.SaveAllAsync();
                return Success<object>(this, comment, "Bình luận thành công", EnumActionApi.Post);
            }
            catch (Exception ex)
            {
                return Error(this, $"Bình luận không thành công: {ex.Message}", 404);
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, CommentDTO commentDTO)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return Error(this, "Không tìm thấy bình luận", 404);
            }

            _mapper.Map(commentDTO, comment);
            await _commentRepository.UpdateAsync(comment);
            return Success<object>(this, null, "Cập nhật bình luận thành công", EnumActionApi.Update);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return Error(this, "Không tìm thấy bình luận", 404);
            }

            await _commentRepository.DeleteAsync(id);
            return Success<object>(this, null, "Xóa bình luận thành công", EnumActionApi.Delete);
        }
        // DELETE: api/Comments
        [HttpDelete("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            foreach (var id in ids)
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                if (comment != null)
                {
                    await _commentRepository.DeleteAsync(id);
                }
            }
            return Success<object>(this, null, "Xóa các bình luận thành công", EnumActionApi.Delete);
        }
    }

}