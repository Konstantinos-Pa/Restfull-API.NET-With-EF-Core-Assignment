using Assignment.Models;
using Assignment.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                var addresses = await _addressRepository.GetAddressesAsync();
                return Ok(addresses);
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
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdAsync(id);
                return Ok(address);
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

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _addressRepository.AddAddressAsync(address);
                    return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, [FromBody] Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _addressRepository.UpdateAddressAsync(id, address);
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            try
            {
                await _addressRepository.DeleteAddressAsync(id);
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
