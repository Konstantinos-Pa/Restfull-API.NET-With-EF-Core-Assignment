using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Candidate
    {
        [Key]
        public int CandidateNumber { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string? LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? NativeLanguage { get; set; }



        public ICollection<Address>? Addresses { get; set; }

        public ICollection<Mobile>? Mobiles { get; set; }

        public PhotoId? PhotoId { get; set; }

        public ICollection<Certificate>? Certificates { get; set; }

    }
}
