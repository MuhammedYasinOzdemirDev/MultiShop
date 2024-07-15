using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Services.Interfaces;

namespace MultiShop.Order.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AddressesController:ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressesController(IAddressService service)
    {
        _addressService = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _addressService.CreateAddressAsync(command);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
    }
            [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressCommand command)
        {
            if (id != command.AddressId)
            {
                return BadRequest("Address ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _addressService.UpdateAddressAsync(command);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new RemoveAddressCommand(id);
                var result = await _addressService.RemoveAddressAsync(command);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetAddressByIdQuery(id);
                var result = await _addressService.GetAddressByIdAsync(query);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _addressService.GetAllAddressesAsync();
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
}