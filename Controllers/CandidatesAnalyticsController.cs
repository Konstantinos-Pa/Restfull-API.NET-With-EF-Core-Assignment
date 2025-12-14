using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Assignment.Repository;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesAnalyticsController : ControllerBase
    {
        private readonly ICandidatesAnalyticsRepository _repository;

        public CandidatesAnalyticsController(ICandidatesAnalyticsRepository repository)
        {
            _repository = repository;
        }

        // GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var candidatesAnalytics = await _repository.GetCandidatesAnalyticsAsync();
                return Ok(candidatesAnalytics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET BY ID
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetId([FromRoute] int id)
        {
            try
            {
                var candidatesAnalytics = await _repository.GetCandidatesAnalyticsByIdAsync(id);
                return Ok(candidatesAnalytics);
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


        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CandidatesAnalytics analytics)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     await _repository.AddCandidatesAnalyticsAsync(analytics);
                    return CreatedAtAction(nameof(GetId), new { id = analytics.Id }, analytics);
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

        // PUT
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CandidatesAnalytics analytics)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.UpdateCandidatesAnalyticsAsync(id, analytics);
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

        // DELETE

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var candidateAnalytics = await _repository.GetCandidatesAnalyticsByIdAsync(id);
                await _repository.DeleteCandidatesAnalyticsAsync(candidateAnalytics);
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
