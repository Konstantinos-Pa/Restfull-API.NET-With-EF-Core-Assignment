using Assignment.Models;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Assignment.Repository
{
    public class MobileRepository:IMobileRepository
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

        public async Task<Mobile> GetMobileByIdAsync(int id)
        {
            if (id == 0)
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
        public async Task AddMobileAsync(Mobile mobile)
        {
            if (mobile == null)
            {
                throw new ArgumentNullException(nameof(mobile));
            }
            await _context.Mobiles.AddAsync(mobile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMobileAsync(int id, Mobile mobile)
        {
            if (id ==0)
            {
                throw new ArgumentNullException(nameof(id) + " is null (Thrown from UpdateMobileAsync)");
            }
            if (mobile == null)
            {
                throw new ArgumentNullException(nameof(mobile), "Mobile is null (from UpdateMobileAsync)");
            }
            var existingMobile = await GetMobileByIdAsync(id);
            // UPDATE logic
            existingMobile.MobileNumber = mobile.MobileNumber;
            existingMobile.MobileType = mobile.MobileType;
            existingMobile.CandidateNumber = mobile.CandidateNumber;
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMobileAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id) + " is zero (Thrown from DeleteMobileAsync)");
            }
            var mobile =  await GetMobileByIdAsync(id);
            _context.Mobiles.Remove(mobile);
            await _context.SaveChangesAsync();
        }
    }
}

