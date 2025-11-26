using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public enum PhoteId
    {
        Natiaonal_Card,
        Passport,
        Driving_License
    }
    public class PhotoId
    {
        public int Id { get; set; }
        public PhoteId PhotoIdImage { get; set; }

        public int PhotoIdNumber { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateOnly DateOfIssue { get; set; }

        // Foreign key to Candidate
        [ForeignKey("Candidate")]
        public int CandidateNumber { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
