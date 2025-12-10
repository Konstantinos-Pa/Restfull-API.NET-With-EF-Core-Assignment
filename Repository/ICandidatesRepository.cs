using Assignment.Models;

namespace Assignment.Repository
{
    public interface ICandidatesRepository
    {
        Task AddCandidateAsync(Candidate candidate);
        Task DeleteCandidateAsync(int id);
        Task<Candidate> GetCandidateByIdAsync(int id);
        Task<List<Candidate>> GetCandidatesAsync();
        Task UpdateCandidateAsync(int id, Candidate candidate);
    }
}