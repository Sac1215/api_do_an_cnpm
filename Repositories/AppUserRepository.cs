using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_do_an_cnpm.data;
using api_do_an_cnpm.Models;
using api_do_an_cnpm.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_do_an_cnpm.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDBContext _context;

        public AppUserRepository(UserManager<AppUser> userManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _userManager.Users
            .ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
            return await _userManager.Users


                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser?> GetByUserNameAsync(string userName)
        {
            return await _userManager.Users


                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task AddAsync(AppUser appUser)
        {
            await _userManager.CreateAsync(appUser);
        }

        public async Task UpdateAsync(AppUser appUser)
        {
            await _userManager.UpdateAsync(appUser);
        }

        public async Task DeleteAsync(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                await _userManager.DeleteAsync(appUser);
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}