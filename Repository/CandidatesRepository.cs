using Assignment.Models;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.ContentModel;
using System.Globalization;

namespace Assignment.Repository
{
    public class CandidatesRepository : ICandidatesRepository
    {
        private readonly ApplicationDbContext _context;
        public CandidatesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Candidate>> GetCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetCandidateByIdAsync(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id) + "ID must be greater than zero. (Thrown from GetCandidateByIdAsync)");
            }
            Candidate? candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
            if (candidate == null)
            {
                throw new Exception("Candidate not found (Thrown from GetCandidateByIdAsync)");
            }
            return candidate;
        }

        public async Task<Candidate> GetCandidateByUserNameAsync(string username)
        {
            if (username.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(username) + " is Null. (Thrown from GetCandidateByUserNameAsync)");
            }
            Candidate? candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.UserName == username);
            if (candidate == null)
            {
                throw new Exception("Candidate not found (Thrown from GetCandidateByIdAsync)");
            }
            return candidate;
        }

        public async Task UpdateCandidateAsync(string id, Candidate candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException(nameof(candidate) + "Is Null (Thrown from UpdateCandidateAsync)");
            }
            else if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id) + "ID must be greater than zero. (Thrown from UpdateCandidateAsync)");
            }
            Candidate existingCandidate = await GetCandidateByIdAsync(id);
            if ((_context.Candidates.FirstOrDefault(c => c.UserName==candidate.UserName))!=null)
            {
                throw new ArgumentException(nameof(candidate.UserName)+" already exists.");
            }
            else
            {
                existingCandidate.UserName = candidate.UserName;
            }
            existingCandidate.FirstName = candidate.FirstName;
            existingCandidate.LastName = candidate.LastName;
            existingCandidate.MiddleName = candidate.MiddleName;
            existingCandidate.Gender = candidate.Gender;
            existingCandidate.DateOfBirth = candidate.DateOfBirth;
            existingCandidate.Email = candidate.Email;
            existingCandidate.NativeLanguage = candidate.NativeLanguage;
            existingCandidate.PhoneNumber = candidate.PhoneNumber;

        await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidateAsync(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from DeleteCandidateAsync)");
            }
            Candidate existingCandidate = await GetCandidateByIdAsync(id);
            _context.Candidates.Remove(existingCandidate);

            await _context.SaveChangesAsync();
        }

        public async Task AddCandidatesCertificateAsync(string candidateId, int certificateId)
        {
            if (candidateId.IsNullOrEmpty())
                throw new ArgumentOutOfRangeException(nameof(candidateId));

            if (certificateId <= 0)
                throw new ArgumentOutOfRangeException(nameof(certificateId));

            var candidate = await _context.Candidates
                .Include(c => c.Certificates) // Ensure collection is loaded
                .FirstOrDefaultAsync(c => c.Id == candidateId);

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

        public async Task RemoveCandidatesCertificateAsync(string candidateId, int certificateId)
        {
            if (candidateId.IsNullOrEmpty())
                throw new ArgumentOutOfRangeException(nameof(candidateId));

            if (certificateId <= 0)
                throw new ArgumentOutOfRangeException(nameof(certificateId));

            var candidate = await _context.Candidates
                .Include(c => c.Certificates) // Ensure collection is loaded
                .FirstOrDefaultAsync(c => c.Id == candidateId);

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

        public async Task<IEnumerable<Object>> MarksPerTopicPerCertificateAsync(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id) + "Is Null (Thrown from MarksPerTopicPerCertificateAsync)");
            }
            var certificates = await _context.Candidates
                .Where(c => c.Id == id)
                .Include(c => c.Certificates ?? Enumerable.Empty<Certificate>())
                .ThenInclude(cert => cert.CandidatesAnalytics)
                .SelectMany(c => c.Certificates ?? Enumerable.Empty<Certificate>())
                .ToListAsync();

            if (certificates == null! || !certificates.Any())
            {
                throw new KeyNotFoundException("No certificates found for the candidate (Thrown from MarksPerTopicPerCertificateAsync)");
            }

            var marksPerTopicWithTitle = certificates.Select(c => new {
                CertificateId = c.Id,
                CertificateTitle = c.Title,
                Analytics = c.CandidatesAnalytics?.ToDictionary(
                    ca => ca.TopicDescription ?? "Unknown Topic",
                    ca => ca.AwardedMarks
                )
            }).ToList();

            if (marksPerTopicWithTitle == null! || marksPerTopicWithTitle.Any())
            {
                throw new KeyNotFoundException("No analytics found for the candidate's certificates (Thrown from MarksPerTopicPerCertificateAsync)");
            }

            return marksPerTopicWithTitle;
        }

        public async Task<List<Certificate>> ObtainedCertificatesOfCandidate(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), " must be greater than zero.");
            }

            var candidates = await _context.Candidates
            .Include(c => c.Certificates!)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (candidates == null)
            {
                throw new KeyNotFoundException($"Candidate with id {id} not found.");
            }

            List<Certificate> listOfCertificates = candidates.Certificates?.ToList() ?? new List<Certificate>();
            return listOfCertificates;

        }

        public async Task<List<Certificate>> NotObtainedCertificatesOfCandidate(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), " must be greater than zero.");
            }
            var obtainedCertificates = await ObtainedCertificatesOfCandidate(id);

            var allCertificates = await _context.Certificates.ToListAsync();
            if (allCertificates == null)
            {
                throw new KeyNotFoundException(nameof(allCertificates) + "Is Null (Thrown from NotObtainedCertificatesOfCandicate)");
            }

            var obtainedIds = new HashSet<int>(obtainedCertificates.Select(c => c.Id));

            var notObtainedCertificates = allCertificates
                .Where(c => !(obtainedIds.Contains(c.Id)))
                .ToList() ?? new List<Certificate>();

            return notObtainedCertificates;
        }

        public async Task<List<Certificate>?> GetCertificatesByDateAsync(string candidateId)
        {
            if (candidateId.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(candidateId), " must be greater than zero. (Thrown from GetCertificatesByDateAsync)");
            }

            var candidate = await _context.Candidates
            .Include(c => c.Certificates)
            .FirstOrDefaultAsync(c => c.Id == candidateId);
            if (candidate == null)
            {
                throw new KeyNotFoundException(nameof(candidate));
            }

            var orderedCertificates = candidate.Certificates?
            .OrderByDescending(c => c.ExaminationDate)
            .ToList();

            return orderedCertificates;

        }

        public async Task<List<int>> GetCertificateCountsByDateRangeAsync(string candidateId, string Start, string End)
        {
            List<Certificate> certificates;
            int passed = 0, failed = 0;
            var result = new List<int> { 0, 0 };
            if (candidateId.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(candidateId), " must be greater than zero. (Thrown from GetCertificateCountsByDateRangeAsync)");
            }
            if (Start == "" || Start == null)
            {
                throw new ArgumentNullException(nameof(Start), " must be greater than zero. (Thrown from GetCertificateCountsByDateRangeAsync)");
            }
            if (End == "" || End == null)
            {
                throw new ArgumentNullException(nameof(End), " must be greater than zero. (Thrown from GetCertificateCountsByDateRangeAsync)");
            }

            if (DateOnly.TryParseExact(Start, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly dateS))
            {
                if (DateOnly.TryParseExact(End, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly dateE))
                {
                    certificates = await _context.Candidates.Where(c => c.Id == candidateId).SelectMany(c => c.Certificates ?? Enumerable.Empty<Certificate>()).Where(c => c.ScoreReportDate >= dateS && c.ScoreReportDate <= dateE).ToListAsync();
                }
                else
                {

                    throw new ArgumentException(End + " can't be parsed to dd/MM/yyyy format.");
                }
            }
            else
            {
                throw new ArgumentException(Start + " can't be parsed to dd/MM/yyyy format.");
            }
            if (certificates != null)
            {
                foreach (var cert in certificates)
                {
                    if (cert.AssessmentResultLabel == true)
                    {
                        passed += 1;
                    }
                    else
                    {
                        failed += 1;
                    }
                }
            }
            return new List<int> { passed, failed };
        }
    }
}