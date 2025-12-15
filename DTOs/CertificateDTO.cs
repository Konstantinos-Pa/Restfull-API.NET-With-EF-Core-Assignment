using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public class CertificateDTO
    {

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        public string? AssessmentTestCode { get; set; }

        public DateOnly ExaminationDate { get; set; }
        
        public DateOnly ScoreReportDate { get; set; }

        public int CandidateScore { get; set; }

        public int MaximumScore { get; set; }

        public int PercentageScore { get; set; }

        public bool AssessmentResultLabel { get; set; }
       
        
    }
}
