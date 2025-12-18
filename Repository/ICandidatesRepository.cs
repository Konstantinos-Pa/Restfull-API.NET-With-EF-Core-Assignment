
using Assignment.Models;
using Assignment.DTOs;

namespace Assignment.Repository
{
    public interface ICandidatesRepository
    {
        Task DeleteCandidateAsync(string id);
        Task<Candidate> GetCandidateByIdAsync(string id);
        Task<List<Candidate>> GetCandidatesAsync();
        Task UpdateCandidateAsync(string id, Candidate candidate);
        Task<IEnumerable<Object>> MarksPerTopicPerCertificateAsync(string id);
        Task<List<Certificate>> ObtainedCertificatesOfCandidate(string candidateNumber);
        Task<List<Certificate>> NotObtainedCertificatesOfCandidate(string candidateNumber);
        Task<List<Certificate>?> GetCertificatesByDateAsync(string candidateId);
        Task<List<int>> GetCertificateCountsByDateRangeAsync(string candidateId, string Start, string End);

    }
}
