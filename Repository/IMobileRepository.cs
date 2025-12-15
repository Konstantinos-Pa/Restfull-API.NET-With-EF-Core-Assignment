using Assignment.DTOs;
using Assignment.Models;

namespace Assignment.Repository

{
    public interface IMobileRepository
    {
        Task<List<Mobile>> GetMobilesAsync();
        Task<Mobile> GetMobileByIdAsync(int id);
        Task<int> AddMobileAsync(Mobile mobile);
        Task UpdateMobileAsync(int id,Mobile mobile);
        Task DeleteMobileAsync(int id);
    }
}