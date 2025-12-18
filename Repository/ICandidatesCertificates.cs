using Assignment.Service;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public interface ICandidatesCertificates
    {
        Task AddCandidatesCertificateAsync(string candidateId, int certificateId);
        Task RemoveCandidatesCertificateAsync(string candidateId, int certificateId);
    }
}
