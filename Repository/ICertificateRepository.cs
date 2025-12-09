using Assignment.Models;
namespace Assignment.Repository;

   public interface ICertificateRepository
    {

    Task<List<Certificate>> GetCertificateAsync();
    Task DeleteCertificateAsync(int id);
    Task<Certificate> GetCertificateAsync(int id);
    Task PutCertificateAsync(int id, Certificate certificate);
    Task PostCertificateAsync(Certificate certificate);

    }
