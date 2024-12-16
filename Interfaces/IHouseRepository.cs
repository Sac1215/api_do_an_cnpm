using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_do_an_cnpm.Models;

namespace api_do_an_cnpm.Interfaces
{
    public interface IHouseRepository
    {
        IQueryable<House> GetAll();
        Task<IEnumerable<House>> GetAllAsync();
        Task<House?> GetByIdAsync(int id);
        Task AddAsync(House house);
        Task UpdateAsync(House house);
        Task DeleteAsync(int id);
        Task<bool> SaveAllAsync();
    }
}