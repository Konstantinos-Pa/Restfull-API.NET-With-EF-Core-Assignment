
using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public class MobileDTO
    {
        public int Id { get; set; }

        [Required]
        public int MobileNumber { get; set; }

        public string? MobileType { get; set; }

        [Required]
        public string? CandidateId { get; set; }
    }
}
