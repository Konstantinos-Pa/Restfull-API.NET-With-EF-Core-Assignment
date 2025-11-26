using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public class CandidatesAnalytics
    {
        public int Id { get; set; }

        public string? TopicDescription { get; set; }

        public int AwardedMarks { get; set; }

        public int PossibleMarks { get; set; }

        // Foreign Key for Certificate

        public int CertificateId { get; set; }  
        public Certificate? Certificate { get; set; }

    }
}
