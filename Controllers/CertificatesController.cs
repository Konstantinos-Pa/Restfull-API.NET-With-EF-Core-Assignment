using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Assignment.Repository;


namespace Assignment.Controllers;

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
            var certificates = await _context.GetCertificateAsync();
            return Ok(certificates);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
     
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCertificate(int id)
    {
        try
        {
            var certificate = await _context.GetCertificateAsync(id);
            return Ok(certificate);
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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCertificate(int certificateId, Certificate certificate)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.PutCertificateAsync(certificateId, certificate);

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
    public async Task<IActionResult> PostCertificate(Certificate certificate)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.PostCertificateAsync(certificate);

            return CreatedAtAction(nameof(GetCertifiicate), new { id = certificate.Id }, certificate);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    // DELETE: api/certificates/id
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCertificate(int id)
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