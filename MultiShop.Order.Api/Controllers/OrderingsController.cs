using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Handlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;
using MultiShop.Order.Application.Services.Interfaces;

namespace MultiShop.Order.Api.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderingsController : ControllerBase
    {
        private readonly IOrderingService _orderingService;

        public OrderingsController(IOrderingService orderingService)
        {
            _orderingService = orderingService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderingCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _orderingService.CreateOrderingAsync(command);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderingCommand command)
        {
            if (id != command.OrderingId)
            {
                return BadRequest("Ordering ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _orderingService.UpdateOrderingAsync(command);
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
                var command = new RemoveOrderingCommand(id);
                var result = await _orderingService.RemoveOrderingAsync(command);
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
                var query = new GetOrderingByIdQuery(id);
                var result = await _orderingService.GetOrderingByIdAsync(query);
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
                var result = await _orderingService.GetAllOrderingsAsync();
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