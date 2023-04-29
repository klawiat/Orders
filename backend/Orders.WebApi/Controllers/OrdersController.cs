using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Models.DTOs;
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
        readonly IUnivercalService<OrderDTO> orderService;
        readonly IMapper mapper;
        public OrdersController(IUnivercalService<OrderDTO> orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        // GET: OrderController/orders
        [HttpGet("Orders/")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var responce = await orderService.GetAllAsync();
                if (responce.StatusCode == HttpStatusCode.OK)
                {
                    List<OrderViewModel> orders = mapper.Map<List<OrderViewModel>>(responce.Data);
                    return Ok(orders);
                }
                else
                    return StatusCode((int)responce.StatusCode, new { result = false, description = responce.Description });
            }
            catch (Exception)
            {
                return BadRequest(new { description = "Непредвиденная ошибка" });
            }
        }

        // GET: OrderController/Orders/id
        [HttpGet("Orders/{id}")]
        public async Task<ActionResult> Details([FromRoute] string id)
        {
            try
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
                    return StatusCode((int)responce.StatusCode, new { description = responce.Description });
            }
            catch (Exception)
            {
                return BadRequest(new { description = "Непредвиденная ошибка" });
            }
        }

        // POST: OrderController/Orders
        [HttpPost("Orders/")]
        public async Task<ActionResult> Create([FromBody] OrderCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OrderDTO order = mapper.Map<OrderDTO>(model);
                    var responce = await orderService.CreateAsync(order);
                    if (responce.StatusCode == HttpStatusCode.OK)
                        return Ok(mapper.Map<OrderViewModel>(responce.Data));
                    else
                        return StatusCode((int)responce.StatusCode, new { description = responce.Description });
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return BadRequest(new { description = "Непредвиденная ошибка" });
            }
        }

        // GET: OrderController/Orders/id
        [HttpPut("Orders/{id}")]
        public async Task<ActionResult> Edit([FromRoute] string id, [FromBody] OrderEditViewModel model)
        {
            try
            {
                Guid orderId;
                if (!Guid.TryParse(id, out orderId))
                    return BadRequest();
                if (ModelState.IsValid)
                {
                    OrderDTO order = mapper.Map<OrderDTO>(model);
                    order.Id = orderId;
                    var responce = await orderService.UpdateAsync(order);
                    if (responce.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(mapper.Map<OrderViewModel>(order));
                    }
                    else
                        return StatusCode((int)responce.StatusCode, new { description = responce.Description });
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { description = "Непредвиденная ошибка" });
            }
        }

        // DELETE: OrderController/Orders/id
        [HttpDelete("Orders/{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            try
            {
                Guid orderId;
                if (!Guid.TryParse(id, out orderId))
                    return BadRequest();
                var responce = await orderService.DeleteAsync(orderId);
                if (responce.StatusCode == HttpStatusCode.OK)
                    return Ok();
                else
                    return StatusCode((int)responce.StatusCode, new { description = responce.Description });
            }
            catch (Exception)
            {
                return BadRequest(new { description = "Непредвиденная ошибка" });
            }

        }
    }
}
