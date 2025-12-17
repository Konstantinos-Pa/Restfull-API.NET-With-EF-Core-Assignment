using Microsoft.AspNetCore.Mvc;
using Assignment.Repository;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesCertificatesController : ControllerBase
    {
        private readonly ICandidatesCertificates _repository;

        public CandidatesCertificatesController(ICandidatesCertificates repository)
        {
            _repository = repository;
        }


        //[HttpPatch("{CandidateId:int}/{CertificateId}")]

    }
}
