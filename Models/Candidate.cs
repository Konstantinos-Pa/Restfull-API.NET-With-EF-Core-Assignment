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
        public int CandidateNumber { get; set; }

        public string? FirstName { get; set; }

        public string MiddleName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public Gender Gender { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? NativeLanguage { get; set; }


        //Navigational Property
        public ICollection<Address>? Addresses { get; set; }

        public ICollection<Mobile>? Mobiles { get; set; }

        public PhotoId? PhotoId { get; set; }

        public ICollection<Certificate>? Certificates { get; set; }

      

    }
}
