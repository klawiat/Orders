using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orders.WebApi.Models.ViewModels;
using Oreders.Domain.Entity;
using Oreders.Domain.Interfaces.Services;
using System.Net;
using System.Text.Json;
//using System.Web.Http;

namespace Orders.WebApi.Controllers
{
    public class OrdersController : Controller
    {
        readonly IOrderService orderService;
        readonly IMapper mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        // GET: OrderController/orders
        [HttpGet("Orders/")]
        public async Task<ActionResult> Index()
        {
            var responce = await orderService.GetAllAsync();
            if (responce.StatusCode == HttpStatusCode.OK)
            {
                List<OrderViewModel> orders = mapper.Map<List<OrderViewModel>>(responce.Data);
                return Ok(orders);
            }
            else
                return StatusCode((int)responce.StatusCode, responce.Description);
        }

        // GET: OrderController/Orders/id
        [HttpGet("Orders/{id}")]
        public async Task<ActionResult> Details([FromRoute] string id)
        {
            Guid orderId;
            if (!Guid.TryParse(id, out orderId))
                return BadRequest();
            var responce = await orderService.GetByIdAsync(orderId);
            if (responce.StatusCode == HttpStatusCode.OK)
            {
                OrderViewModel model = mapper.Map<OrderViewModel>(responce.Data);
                return Ok(model);
            }
            else
                return StatusCode((int)responce.StatusCode, responce.Description);
        }

        // POST: OrderController/Orders
        [HttpPost("Orders/")]
        public async Task<ActionResult> Create([FromBody] OrderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Order order = mapper.Map<Order>(model);
                var responce = await orderService.CreateAsync(order);
                if (responce.StatusCode == HttpStatusCode.OK)
                    return Ok(mapper.Map<OrderViewModel>(responce.Data));
                else
                    return StatusCode((int)responce.StatusCode, responce.Description);
            }
            else
            {
                ModelState.AddModelError("", "BadRequest");
                return BadRequest();
            }
        }

        // GET: OrderController/Orders/id
        [HttpPut("Orders/{id}")]
        public async Task<ActionResult> Edit([FromRoute] string id, [FromBody] OrderEditViewModel model)
        {
            Guid orderId;
            if (!Guid.TryParse(id, out orderId))
                return BadRequest();
            model.Id = orderId;
            if (ModelState.IsValid)
            {
                Order order = mapper.Map<Order>(model);
                var responce = await orderService.UpdateAsync(order);
                if (responce.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(mapper.Map<OrderViewModel>(order));
                }
                else
                    return StatusCode((int)responce.StatusCode, responce.Description);
            }
            else
            {
                ModelState.AddModelError("", "BadRequest");
                return BadRequest();
            }
        }

        // DELETE: OrderController/Orders/id
        [HttpDelete("Orders/{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            Guid orderId;
            if (!Guid.TryParse(id, out orderId))
                return BadRequest();
            var responce = await orderService.DeleteAsync(orderId);
            if (responce.StatusCode == HttpStatusCode.OK)
                return Ok();
            else
                return StatusCode((int)responce.StatusCode, responce.Description);
        }
    }
}
