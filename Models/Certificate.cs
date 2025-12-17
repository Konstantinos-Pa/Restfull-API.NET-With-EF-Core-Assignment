
namespace Assignment.Models
{
    public class Certificate
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? AssessmentTestCode { get; set; }

        public DateOnly ExaminationDate { get; set; }

        public DateOnly ScoreReportDate { get; set; }

        public int CandidateScore { get; set; }

        public int MaximumScore { get; set; }

        public int PercentageScore { get; set; }

        public bool AssessmentResultLabel { get; set; }

        //navigational properties
        public ICollection<Candidate>? Candidates { get; set; }

        public ICollection<CandidatesAnalytics>? CandidatesAnalytics { get; set; }


    }
}
