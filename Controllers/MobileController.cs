using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Assignment.DTOs;
using Assignment.Repository;
using Mapster;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IMobileRepository _mobileRepository;

        public MobileController(IMobileRepository mobileRepository)
        {
            _mobileRepository = mobileRepository;
        }

        // --------------------------------------------------------
        // GET: api/Mobile
        // --------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMobiles()
        {
            try
            {
                var mobiles = await _mobileRepository.GetMobilesAsync();
                return Ok(mobiles.Adapt<List<MobileDTO>>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // --------------------------------------------------------
        // GET: api/Mobile/5
        // --------------------------------------------------------
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMobileId([FromRoute] int id)
        {
            try
            {
                var mobile = await _mobileRepository.GetMobileByIdAsync(id);
                return Ok(mobile.Adapt<MobileDTO>());
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

        // --------------------------------------------------------
        // POST: api/Mobile
        // --------------------------------------------------------
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostMobile([FromBody] MobileDTO mobileDTO)
        {
            try
            {
                var mobile = mobileDTO.Adapt<Mobile>();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int Id = await _mobileRepository.AddMobileAsync(mobile);
                var resultDto = mobile.Adapt<MobileDTO>();

                return CreatedAtAction(
                    nameof(GetMobileId),
                    new { Id },
                    resultDto
                );

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // --------------------------------------------------------
        // PUT: api/Mobile/5
        // --------------------------------------------------------
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMobile([FromRoute] int id, [FromBody] MobileDTO mobileDTO)
        {
            try
            {
                var mobile = mobileDTO.Adapt<Mobile>();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _mobileRepository.UpdateMobileAsync(id, mobile);
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

        // --------------------------------------------------------
        // DELETE: api/Mobile/5
        // --------------------------------------------------------
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMobile([FromRoute] int id)
        {
            try
            {
                await _mobileRepository.DeleteMobileAsync(id);
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
