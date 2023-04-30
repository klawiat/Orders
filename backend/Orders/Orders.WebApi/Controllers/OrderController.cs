using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Interfaces.Services;
using Orders.Application.Models.DTOs;
using Orders.WebApi.Models.ViewModels;

namespace Orders.WebApi.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService service;
        private readonly IMapper mapper;
        public OrderController(IOrderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        [HttpGet("orders/")]
        public async Task<IActionResult> GetOrders()
        {
            IEnumerable<OrderDTO> orders = await service.GetOrders();
            return Ok(mapper.Map<List<OrderViewModel>>(orders));
        }
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] string id)
        {
            OrderDTO order = await service.GetOrderById(Guid.Parse(id));
            return Ok(mapper.Map<OrderViewModel>(order));
        }
        [HttpPost("orders/")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderViewModel orderVM)
        {
            OrderDTO orderDto = mapper.Map<OrderDTO>(orderVM);
            await service.CreateOrder(orderDto);
            return Ok(mapper.Map<OrderViewModel>(orderDto));
        }
        [HttpPut("orders/{id}")]
        public async Task<IActionResult> EditOrder([FromRoute] string id, [FromBody] EditOrderViewModel orderVM)
        {
            OrderDTO orderDto = mapper.Map<OrderDTO>(orderVM);
            orderDto.Id = Guid.Parse(id);
            //orderDto.Id = id;
            await service.UpdateOrder(orderDto);
            return Json(mapper.Map<OrderViewModel>(orderDto));
        }
        [HttpDelete("orders/{id}")]
        public async Task<IActionResult> EditOrder([FromRoute] string id)
        {
            await service.DeleteOrder(Guid.Parse(id));
            return Ok();
        }
    }
}
