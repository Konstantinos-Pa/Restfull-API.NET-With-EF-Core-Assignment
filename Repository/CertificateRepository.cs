using Assignment.Models;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository
{
    public class CertificatesRepository : ICertificateRepository
    {
        private readonly PostgresDbContext _context;

        public CertificatesRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<Certificate>> GetCertificateAsync()
        {
            return await _context.Certificates.ToListAsync();
        }

        public async Task<Certificate> GetCertificateAsync(int id)
        {
            if (id == 0 || id == null)
            {
                throw new ArgumentNullException(nameof(id) + " is null or zero (Thrown from GetCertificateAsync)");
            }

            Certificate? certificate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.Id == id);

            if (certificate == null)
            {
                throw new Exception("Certificate not found (Thrown from GetCertificateAsync)");
            }

            return certificate;
        }

        public async Task PostCertificateAsync(Certificate certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate) + " is null (Thrown from PostCertificateAsync)");
            }

            await _context.Certificates.AddAsync(certificate);
            await _context.SaveChangesAsync();
        }

        public async Task PutCertificateAsync(int id, Certificate certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate) + " is null (Thrown from PutCertificateAsync)");
            }

            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id) + " is zero (Thrown from PutCertificateAsync)");
            }

            Certificate existingCertificate = await GetCertificateAsync(id);

            // Update fields
            existingCertificate.Title = certificate.Title;
            existingCertificate.AssessmentTestCode = certificate.AssessmentTestCode;
            existingCertificate.ExaminationDate = certificate.ExaminationDate;
            existingCertificate.ScoreReportDate = certificate.ScoreReportDate;
            existingCertificate.CandidateScore = certificate.CandidateScore;
            existingCertificate.MaximumScore = certificate.MaximumScore;
            existingCertificate.PercentageScore = certificate.PercentageScore;
            existingCertificate.AssessmentResultLabel = certificate.AssessmentResultLabel;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCertificateAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id) + " is zero (Thrown from DeleteCertificateAsync)");
            }

            Certificate existingCertificate = await GetCertificateAsync(id);

            _context.Certificates.Remove(existingCertificate);
            await _context.SaveChangesAsync();
        }
    }
}
