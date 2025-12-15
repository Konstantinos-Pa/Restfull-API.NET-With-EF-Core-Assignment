using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public class Mobile
    {
        public int Id { get; set; }

        public int MobileNumber { get; set; }

        public string? MobileType { get; set; }

        public int CandidateNumber { get; set; }
        //Nagigational properties
        public Candidate? Candidate { get; set; }    
    }
}
