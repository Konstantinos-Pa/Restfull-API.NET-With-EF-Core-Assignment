using Assignment.Service;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public interface ICandidatesCertificates
    {
        Task AddCandidatesCertificateAsync(int candidateId, int certificateId);
        Task RemoveCandidatesCertificateAsync(int candidateId, int certificateId);
    }
}
