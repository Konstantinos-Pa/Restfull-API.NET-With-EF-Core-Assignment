
using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public enum Gender
    {
        Male,
        Female
    }

    public class CandidateDTO
    {
        public int CandidateNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [MaxLength(20)]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string? LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string? NativeLanguage { get; set; }

        public List<int>? Certificates { get; set; } = new List<int>();
    }
}
