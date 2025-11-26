using Assignment.Models;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Assignment.Repository
{
    public class MobileRepository
    {
        private readonly PostgresDbContext _context;

        public MobileRepository(PostgresDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<Mobile>> GetMobilesAsync()
        {
            return await _context.Mobiles.ToListAsync();
        }

        public async Task<Mobile?> GetMobileByIdAsync(int id)
        {
            if (id == null || id == 0)
            {
                throw new ArgumentNullException(nameof(id) + "Is Null (Thrown from GetMobileByIdAsync)");
            }
            Mobile? mobile = await _context.Mobiles.FirstOrDefaultAsync(m => m.Id == id);
            if (mobile == null)
            {
                throw new Exception("Mobile not found (Thrown from GetMobileByIdAsync)");
            }
            return mobile;
        }

        //public async Task<Mobile> AddMobileAsync(int id,Mobile mobile)
        //{
        //    if (id == null || id == 0)
        //    {
        //        throw new ArgumentNullException(nameof(id) + "Is Null (Thrown from UpdateAddressAsyncc)");
        //    }
        //    else if (mobile == null)
        //    {
        //        throw new ArgumentNullException(nameof(mobile) + "Is Null (Thrown from UpdateAddressAsync)");
        //    }
        //    await _context.SaveChangesAsync();
        //}

    }
}
