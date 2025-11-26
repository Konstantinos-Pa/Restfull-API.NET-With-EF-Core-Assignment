using Assignment.Models;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository
{
    public class CandidatesAnalyticsRepository
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


    }
}
