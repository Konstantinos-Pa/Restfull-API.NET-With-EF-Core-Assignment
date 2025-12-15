
using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public class AddressDTO
    {

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Street { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        [MaxLength(5)]
        public int PostalCode { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public int LandlineNumber { get; set; }

        [Required]
        public int CandidateNumber { get; set; }
    }
}
