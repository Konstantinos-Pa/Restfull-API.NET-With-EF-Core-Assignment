using Assignment.Models;
using Assignment.DTOs;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from GetAddressByIdAsync)");
            }
            Address? address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
            {
                throw new Exception("Address not found (Thrown from GetAddressByIdAsync)");
            }
            return address;
        }

        public async Task<int> AddAddressAsync(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address) + " Is Null (Thrown from AddAddressAsync)");
            }
            if (address.CandidateId.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(address.CandidateId) + " Is Null (Thrown from AddAddressAsync)");
            }
            Candidate? candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == address.CandidateId);
            if (candidate == null)
            {
                throw new ArgumentException("Didn't find any candidates specified");
            }
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address.Id;
        }

        public async Task UpdateAddressAsync(int id, Address address)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from UpdateAddressAsyncc)");
            }
            else if (address == null)
            {
                throw new ArgumentNullException(nameof(address) + "Is Null (Thrown from UpdateAddressAsync)");
            }
            Address existingAddress = await GetAddressByIdAsync(id);
            existingAddress.City = address.City;
            existingAddress.Street = address.Street;
            existingAddress.State = address.State;
            existingAddress.PostalCode = address.PostalCode;
            existingAddress.Country = address.Country;
            existingAddress.LandlineNumber = address.LandlineNumber;
            existingAddress.CandidateId = address.CandidateId;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from DeleteAddressAsync)");
            }
            Address address = await GetAddressByIdAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}