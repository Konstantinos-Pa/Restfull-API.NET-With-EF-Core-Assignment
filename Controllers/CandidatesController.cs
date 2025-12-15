using Assignment.Models;
using Assignment.DTOs;
using Assignment.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Assignment.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesRepository _candidatesRepository;
        public CandidatesController(ICandidatesRepository candidatesRepository)
        {
            _candidatesRepository = candidatesRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                var candidates = await _candidatesRepository.GetCandidatesAsync();
                return Ok(candidates.Adapt<List<CandidateDTO>>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCandidateById([FromRoute] int id)
        {
            try
            {
                var candidate = await _candidatesRepository.GetCandidateByIdAsync(id);
                return Ok(candidate.Adapt<CandidateDTO>());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCandidate([FromBody] CandidateDTO candidateDTO)
        {
            try
            {
                var candidate = candidateDTO.Adapt<Candidate>();
                if (ModelState.IsValid)
                {
                    int Id = await _candidatesRepository.AddCandidateAsync(candidate);
                    var resultDto = candidate.Adapt<CandidateDTO>();

                    return CreatedAtAction(
                        nameof(GetCandidateById),
                        new { Id },
                        resultDto
                    );
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCandidate([FromRoute] int id, [FromBody] CandidateDTO candidateDTO)
        {
            try
            {
                var candidate = candidateDTO.Adapt<Candidate>();
                if (ModelState.IsValid)
                {
                    await _candidatesRepository.UpdateCandidateAsync(id, candidate);
                    return NoContent();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCandidate([FromRoute] int id)
        {
            try
            {
                await _candidatesRepository.DeleteCandidateAsync(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/MarksPerCertificatesOfCandidate/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMarksPerTopicPerCertificateByCandidate([FromRoute] int candidateNumber)
        {
            try
            {
                var certificates = await _candidatesRepository.MarksPerTopicPerCertificateAsync(candidateNumber);

                return Ok(certificates.Adapt<List<CertificateDTO>>());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("/CertificatesOfCandidate/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetObtainedCertificatesByCandidate([FromRoute] int candidateNumber)
        {
            try
            {
                var certificates = await _candidatesRepository.ObtainedCertificatesOfCandidate(candidateNumber);

                return Ok(certificates.Adapt<List<CertificateDTO>>());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("/NotCertificatesOfCandidate/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotObtainedCertificatesByCandidate([FromRoute] int id)
        {
            try
            {
                var certificates = await _candidatesRepository.NotObtainedCertificatesOfCandidate(id);

                return Ok(certificates.Adapt<List<CertificateDTO>>());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/CertificatesByDate/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCertificatesByDate([FromRoute] int id)
        {
            try
            {
                var certificates = await _candidatesRepository.GetCertificatesByDateAsync(id);

                return Ok(certificates.Adapt<List<CertificateDTO>>());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/CertificatesCountByDateRange/{id:int}/{StartD:alpha}/{EndD:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCertificateCountsByDateRange([FromRoute] int id, [FromRoute] string StartD, [FromRoute] string EndD)
        {
            try
            {
                var certificates = await _candidatesRepository.GetCertificateCountsByDateRangeAsync(id,StartD,EndD);

                return Ok(certificates.Adapt<List<CertificateDTO>>());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
