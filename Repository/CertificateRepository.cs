
using Assignment.Models;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository
{
    public class CertificatesRepository : ICertificateRepository
    {
        private readonly ApplicationDbContext _context;

        public CertificatesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Certificate>> GetCertificatesAsync()
        {
            return await _context.Certificates.ToListAsync();
        }

        public async Task<Certificate> GetCertificateByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from GetCertificateByIdAsync)");
            }

            Certificate? certificate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.Id == id);

            if (certificate == null)
            {
                throw new Exception("Certificate not found (Thrown from GetCertificateByIdAsync)");
            }

            return certificate;
        }

        public async Task<int> AddCertificateAsync(Certificate certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate) + " is null (Thrown from AddCertificateAsync)");
            }

            await _context.Certificates.AddAsync(certificate);
            await _context.SaveChangesAsync();
            return certificate.Id;
        }

        public async Task UpdateCertificateAsync(int id, Certificate certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate) + " is null (Thrown from UpdateCertificateAsync)");
            }

            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from UpdateCertificateAsync)");
            }

            Certificate existingCertificate = await GetCertificateByIdAsync(id);

            // Update fields
            existingCertificate.Title = certificate.Title;
            existingCertificate.AssessmentTestCode = certificate.AssessmentTestCode;
            existingCertificate.Description = certificate.Description;
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
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from DeleteCertificateAsync)");
            }

            Certificate existingCertificate = await GetCertificateByIdAsync(id);

            _context.Certificates.Remove(existingCertificate);
            await _context.SaveChangesAsync();
        }




    }





}
