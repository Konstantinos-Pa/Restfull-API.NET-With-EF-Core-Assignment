using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public class Address
    {
        public int Id { get; set; }

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

        // Foreign key to Candidate
        [ForeignKey(nameof(CandidateNumber))]
        public int CandidateNumber { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
