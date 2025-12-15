
using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public class MobileDTO
    {
        [Required]
        public int MobileNumber { get; set; }

        public string? MobileType { get; set; }

        [Required]
        public int CandidateNumber { get; set; }
    }
}
