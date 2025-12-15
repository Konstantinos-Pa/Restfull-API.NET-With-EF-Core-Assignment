using Assignment.DTOs;
using Assignment.Models;
using Assignment.Repository;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateRepository _context;

        public CertificatesController(ICertificateRepository context)
        {
            _context = context;
        }

        // GET: api/Certificates
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCertifiicate()
        {
            try
            {
                var certificates = await _context.GetCertificatesAsync();
                return Ok(certificates.Adapt<List<CertificateDTO>>());
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
        public async Task<IActionResult> GetCertificateById([FromRoute] int id)
        {
            try
            {
                var certificate = await _context.GetCertificateByIdAsync(id);
                return Ok(certificate.Adapt<CertificateDTO>());
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
        public async Task<IActionResult> PutCertificate([FromRoute]int certificateId, [FromBody] CertificateDTO certificateDTO)
        {
            try
            {
                var certificate = certificateDTO.Adapt<Certificate>();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _context.UpdateCertificateAsync(certificateId, certificate);

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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCertificate([FromBody] CertificateDTO certificateDTO)
        {
                var certificate = certificateDTO.Adapt<Certificate>();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int Id = await _context.AddCertificateAsync(certificate);
                var resultDto = certificate.Adapt<CertificateDTO>();

                return CreatedAtAction(
                    nameof(GetCertificateById),
                    new { Id },
                    resultDto
                );
        }


        // DELETE: api/certificates/id
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCertificate([FromRoute] int id)
        {
            try
            {
                await _context.DeleteCertificateAsync(id);
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