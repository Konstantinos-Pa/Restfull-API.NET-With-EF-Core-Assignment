
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public class Certificate
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        public string? AssessmentTestCode { get; set; }


        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateOnly ExaminationDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateOnly ScoreReportDate { get; set; }

        public int CandidateScore { get; set; }

        [Required]
        public int MaximumScore { get; set; }   

        public int PercentageScore { get; set; }

        public bool AssessmentResultLabel { get; set; }

        // Foreign Key for Candidate M-M
        public ICollection<Candidate>? Candidates { get; set; }

        public ICollection<CandidatesAnalytics>? CandidatesAnalytics { get; set; }
       
        
    }
}
