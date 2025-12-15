
using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public enum PhoteId
    {
        Natiaonal_Card,
        Passport,
        Driving_License
    }
    public class PhotoIdDTO
    {
        [Required]
        public PhoteId PhotoIdImage { get; set; }

        [Required]
        public int PhotoIdNumber { get; set; }

        [Required]
        public DateOnly DateOfIssue { get; set; }

        [Required]
        public int CandidateNumber { get; set; }

    }
}
