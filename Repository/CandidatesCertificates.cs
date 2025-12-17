using Assignment.Service;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public class CandidatesCertificates: ICandidatesCertificates
    {
        private readonly ApplicationDbContext _context;
        public CandidatesCertificates(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCandidatesCertificateAsync(int candidateId, int certificateId)
        {
            if (candidateId <= 0)
                throw new ArgumentOutOfRangeException(nameof(candidateId));

            if (certificateId <= 0)
                throw new ArgumentOutOfRangeException(nameof(certificateId));

            var candidate = await _context.Candidates
                .Include(c => c.Certificates) // Ensure collection is loaded
                .FirstOrDefaultAsync(c => c.CandidateNumber == candidateId);

            if (candidate == null)
                throw new InvalidOperationException("Candidate not found.");

            var certificate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.Id == certificateId);

            if (certificate == null)
                throw new InvalidOperationException("Certificate not found.");

            // Initialize collection if null
            candidate.Certificates ??= new List<Certificate>();

            if (!candidate.Certificates.Any(c => c.Id == certificateId))
            {
                candidate.Certificates.Add(certificate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCandidatesCertificateAsync(int candidateId, int certificateId)
        {
            if (candidateId <= 0)
                throw new ArgumentOutOfRangeException(nameof(candidateId));

            if (certificateId <= 0)
                throw new ArgumentOutOfRangeException(nameof(certificateId));

            var candidate = await _context.Candidates
                .Include(c => c.Certificates) // Ensure collection is loaded
                .FirstOrDefaultAsync(c => c.CandidateNumber == candidateId);

            if (candidate == null)
                throw new InvalidOperationException("Candidate not found.");

            var certificate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.Id == certificateId);

            if (certificate == null)
                throw new InvalidOperationException("Certificate not found.");

            // Only remove if collection is not null
            if (candidate.Certificates != null && candidate.Certificates.Any(c => c.Id == certificateId))
            {
                candidate.Certificates.Remove(certificate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
