using Assignment.Models;
namespace Assignment.Repository
{

    public interface ICertificateRepository
    {
        Task<List<Certificate>> GetCertificatesAsync();
        Task DeleteCertificateAsync(int id);
        Task<Certificate> GetCertificateByIdAsync(int id);
        Task UpdateCertificateAsync(int id, Certificate certificate);
        Task AddCertificateAsync(Certificate certificate);



    }
}
