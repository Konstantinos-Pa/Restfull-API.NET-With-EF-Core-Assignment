using Assignment.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationDemo.Authentication
{
    public class AppUser : IdentityUser
    {
        public int? CandidateId { get; set; }
        //Nagigational Property
        public Candidate? Candidate { get; set; }

        public DateOnly? CreatedDate { get; set; }

    }
}
