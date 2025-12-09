using Assignment.Models;
using Assignment.Repository;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("api/candidates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                var candidates = await _candidatesRepository.GetCandidatesAsync();
                return Ok(candidates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("api/candidates/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            try
            {
                var candidate = await _candidatesRepository.GetCandidateByIdAsync(id);
                return Ok(candidate);
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

        [HttpPost("api/candidates")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCandidate([FromBody] Candidate candidate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _candidatesRepository.AddCandidateAsync(candidate);
                    return CreatedAtAction(nameof(GetCandidateById), new { id = candidate.CandidateNumber }, candidate);
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

        [HttpPut("api/candidates/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] Candidate candidate)
        {
            try
            {
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

        [HttpDelete("api/candidates/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCandidate(int id)
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


    }
}
