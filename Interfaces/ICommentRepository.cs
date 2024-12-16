using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_do_an_cnpm.Models;
using static ResponseHelper;

namespace api_do_an_cnpm.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
        Task<bool> SaveAllAsync();
        IQueryable<Comment> GetAll();
        Task<List<Comment>> GetRepliesAsync(int parentCommentId, PageRequest request);
        Task<int> GetTotalRepliesAsync(int parentCommentId);
    }
}