
using Assignment.Models;
using Assignment.DTOs;
using Assignment.Repository;
using Assignment.Service; // εκεί που είναι το PostgresDbContext
using Microsoft.EntityFrameworkCore;
using Mapster;

public class CandidatesAnalyticsRepository: ICandidatesAnalyticsRepository
    {
        private readonly PostgresDbContext _context;

        public CandidatesAnalyticsRepository(PostgresDbContext context)
        {
          _context = context;
        }

        public async Task<List<CandidatesAnalytics>> GetCandidatesAnalyticsAsync()
        {
            return await _context.CandidatesAnalytics.ToListAsync();
        }


        public async Task<CandidatesAnalytics> GetCandidatesAnalyticsByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id), "ID must be greater than zero. (Thrown from GetCandidatesAnalyticsByIdAsync)");
            }

            CandidatesAnalytics? analytics = await _context.CandidatesAnalytics.FirstOrDefaultAsync(a => a.Id == id);

            if (analytics == null)
            {
                throw new Exception("CandidatesAnalytics not found (Thrown from GetCandidatesAnalyticsByIdAsync)");
            }
            return analytics;
        }

       public async Task<int> AddCandidatesAnalyticsAsync(CandidatesAnalytics analytics)
        {
            if (analytics == null)
            {
                throw new ArgumentNullException(nameof(analytics) + " Is Null (Thrown from AddCandidatesAnalyticsAsync)");
            }
            if (analytics.CertificateId == 0)
            {
                throw new ArgumentNullException(nameof(analytics.CertificateId) + " Is Null (Thrown from AddCandidatesAnalyticsAsync)");
            }
            Certificate? certificate = await _context.Certificates.FirstOrDefaultAsync(c => c.Id == analytics.CertificateId);
            if (certificate == null)
            {
                throw new ArgumentException("Didn't find any certificates specified");
            }
            _context.CandidatesAnalytics.Add(analytics);
            await _context.SaveChangesAsync();
            return analytics.Id;
        }

        public async Task UpdateCandidatesAnalyticsAsync(int id, CandidatesAnalytics analytics)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id), "ID must be greater than zero. (Thrown from UpdateCandidatesAnalyticsAsync)");
            }

            if (analytics == null)
            {
                throw new ArgumentNullException(nameof(analytics), "Is Null.  (Thrown from UpdateCandidatesAnalyticsAsync)");
            }

            var existing = await GetCandidatesAnalyticsByIdAsync(id);

            // Update πεδία
            existing.TopicDescription = analytics.TopicDescription;
            existing.AwardedMarks = analytics.AwardedMarks;
            existing.PossibleMarks = analytics.PossibleMarks;
            existing.CertificateId = analytics.CertificateId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidatesAnalyticsAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from DeleteCandidatesAnalyticsAsync)");
            }
            CandidatesAnalytics analytics = await GetCandidatesAnalyticsByIdAsync(id);

            _context.CandidatesAnalytics.Remove(analytics);
            await _context.SaveChangesAsync();
        }
    }

