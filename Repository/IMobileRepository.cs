using Assignment.Models;
namespace Assignment.Repository

{
    public interface IMobileRepository
    {
        Task<List<Mobile>> GetMobilesAsync();
        Task<Mobile> GetMobileByIdAsync(int id);
        Task AddMobileAsync(Mobile mobile);
        Task UpdateMobileAsync(int id,Mobile mobile);
        Task DeleteMobileAsync(int id);
    }
}