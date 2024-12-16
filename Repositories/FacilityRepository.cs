using System.Collections.Generic;
using System.Threading.Tasks;
using api_do_an_cnpm.data;


using api_do_an_cnpm.Interfaces;
using api_do_an_cnpm.Models;
using api_do_an_cnpm.Models.api_do_an_cnpm.Models;
using Microsoft.EntityFrameworkCore;
namespace api_do_an_cnpm.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly ApplicationDBContext _context;
        public FacilityRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Facility>> GetAllAsync()
        {
            return await _context.Facilities.ToListAsync();
        }
        public async Task<Facility?> GetByIdAsync(int id)
        {
            return await _context.Facilities.FindAsync(id);
        }
        public async Task AddAsync(Facility facility)
        {
            await _context.Facilities.AddAsync(facility);
        }

        public async Task AddAsync(List<Facility> facility)
        {
            await _context.Facilities.AddRangeAsync(facility);
        }

        public async Task UpdateAsync(Facility facility)
        {
            _context.Facilities.Update(facility);
        }

        public async Task UpdateAsync(List<Facility> facility)
        {
            _context.Facilities.UpdateRange(facility);
        }

        public async Task DeleteAsync(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility != null)
            {
                _context.Facilities.Remove(facility);
            }
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

