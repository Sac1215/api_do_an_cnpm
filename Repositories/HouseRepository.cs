using System.Collections.Generic;
using System.Threading.Tasks;
using api_do_an_cnpm.data;
using api_do_an_cnpm.Interfaces;
using api_do_an_cnpm.Models;
using Microsoft.EntityFrameworkCore;
namespace api_do_an_cnpm.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly ApplicationDBContext _context;
        public HouseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public IQueryable<House> GetAll()
        {
            return _context.Houses;
        }


        public async Task<IEnumerable<House>> GetAllAsync()
        {
            return await _context.Houses
              .Include(h => h.Owner)
               .Include(h => h.Facilities)
               .Include(h => h.Comments)
                 .ThenInclude(c => c.User)
               .ToListAsync();
        }
        public async Task<House?> GetByIdAsync(int id)
        {
            return await _context.Houses
                .Include(h => h.Facilities)
                .Include(h => h.Comments)
                .ThenInclude(r => r.Replies)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task AddAsync(House house)
        {
            await _context.Houses.AddAsync(house);
        }
        public async Task UpdateAsync(House house)
        {
            _context.Houses.Update(house);
        }
        public async Task DeleteAsync(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            if (house != null)
            {
                _context.Houses.Remove(house);
            }
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}