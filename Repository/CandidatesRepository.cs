using Assignment.Service;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository
{
    public class CandidatesRepository
    {
        private readonly PostgresDbContext _context;
        public CandidatesRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<Candidate>> GetCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<Candidate?> GetCandidateByIdAsync(int id)
        {
            if (id == null || id == 0)
            {
                throw new ArgumentNullException(nameof(id) + "Is Null (Thrown from GetCandidateByIdAsync)");
            }
            Candidate? candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.CandidateNumber == id);
            if (candidate == null) {
                throw new Exception("Candidate not found (Thrown from GetCandidateByIdAsync)");
            }
                return candidate;
        }

        public async Task AddCandidateAsync(Candidate candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException(nameof(candidate)+ "Is Null (Thrown from AddCandidateAsync)");
            }
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateCandidateAsync(int id,Candidate candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException(nameof(candidate) + "Is Null (Thrown from UpdateCandidateAsync)");
            }
            else if (id == null || id == 0)
            {
                throw new ArgumentNullException(nameof(id) + "Is Null (Thrown from UpdateCandidateAsync)");
            }
            Candidate? existingCandidate = await GetCandidateByIdAsync(id);
            existingCandidate.FirstName = candidate.FirstName;
            existingCandidate.LastName = candidate.LastName;
            existingCandidate.MiddleName = candidate.MiddleName;
            existingCandidate.Gender = candidate.Gender;
            existingCandidate.DateOfBirth = candidate.DateOfBirth;
            existingCandidate.Email = candidate.Email;
            existingCandidate.NativeLanguage = candidate.NativeLanguage;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidateAsync(int id)
        {
            if (id == null || id == 0)
            {
                throw new ArgumentNullException(nameof(id) + "Is Null (Thrown from DeleteCandidateAsync)");
            }
            Candidate? existingCandidate = await GetCandidateByIdAsync(id);
            _context.Candidates.Remove(existingCandidate);

            await _context.SaveChangesAsync();
        }

    }
}
