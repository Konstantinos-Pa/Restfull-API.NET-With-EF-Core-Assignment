
using Assignment.Models;
using Assignment.DTOs;

namespace Assignment.Repository
{
    public interface ICandidatesRepository
    {
        Task<int> AddCandidateAsync(Candidate candidate);
        Task DeleteCandidateAsync(int id);
        Task<Candidate> GetCandidateByIdAsync(int id);
        Task<List<Candidate>> GetCandidatesAsync();
        Task UpdateCandidateAsync(int id, Candidate candidate);
        Task<IEnumerable<Object>> MarksPerTopicPerCertificateAsync(int id);
        Task<List<Certificate>> ObtainedCertificatesOfCandidate(int candidateNumber);
        Task<List<Certificate>> NotObtainedCertificatesOfCandidate(int candidateNumber);
        Task<List<Certificate>?> GetCertificatesByDateAsync(int candidateId);
        Task<List<int>> GetCertificateCountsByDateRangeAsync(int candidateId, string Start, string End);

    }
}
