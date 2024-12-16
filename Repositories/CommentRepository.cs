using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_do_an_cnpm.data;
using api_do_an_cnpm.Interfaces;
using api_do_an_cnpm.Models;
using Microsoft.EntityFrameworkCore;
using static ResponseHelper;

namespace api_do_an_cnpm.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .Include(c => c.User) // Bao gồm thông tin người dùng
                .Include(c => c.Replies) // Bao gồm thông tin trả lời
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User) // Bao gồm thông tin người dùng
                .Include(c => c.Replies) // Bao gồm thông tin trả lời
                .FirstOrDefaultAsync(c => c.Id == id);

        }


        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment); await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Comment> GetAll()
        {
            return _context.Comments;
        }

        public async Task<List<Comment>> GetRepliesAsync(int parentCommentId, PageRequest request)
        {
            return await _context.Comments
                .Where(c => c.ParentCommentId == parentCommentId)
                .OrderBy(c => c.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalRepliesAsync(int parentCommentId)
        {
            return await _context.Comments
                .Where(c => c.ParentCommentId == parentCommentId)
                .CountAsync();
        }
    }
}