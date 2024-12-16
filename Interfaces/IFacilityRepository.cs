using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_do_an_cnpm.Models.api_do_an_cnpm.Models;

namespace api_do_an_cnpm.Interfaces
{
    public interface IFacilityRepository
    {
        Task<IEnumerable<Facility>> GetAllAsync();
        Task<Facility?> GetByIdAsync(int id);
        Task AddAsync(Facility facility);
        Task AddAsync(List<Facility> facility);


        Task UpdateAsync(Facility facility);
        Task UpdateAsync(List<Facility> facility);
        Task DeleteAsync(int id);
        Task<bool> SaveAllAsync();
    }
}