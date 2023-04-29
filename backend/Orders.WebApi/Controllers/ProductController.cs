using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Orders.Application.Models.DTOs;
using Orders.WebApi.Models.ViewModels;
using Oreders.Domain.Entity;
using Oreders.Domain.Interfaces.Services;
using System.Net;

namespace Orders.WebApi.Controllers
{
    public class ProductController : Controller
    {
        readonly IUnivercalService<ProductDTO> productService;
        readonly IMapper mapper;
        public ProductController(IUnivercalService<ProductDTO> productService,IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }
        // GET: ProductController
        [HttpGet("Product/All")]
        public async Task<ActionResult> GetAll()
        {
            var responce = await productService.GetAllAsync();
            if (responce.StatusCode == HttpStatusCode.OK)
            {
                List<ProductViewModel> model = mapper.Map<List<ProductViewModel>>(responce.Data);
                return Json(model.OrderBy(x=>x.Id));
            }
            else
                return StatusCode((int)responce.StatusCode, responce.Description);
        }

        // GET: Products/id
        [HttpGet("Product/{id}")]
        public async Task<ActionResult> Details([FromRoute]string id)
        {
            Guid productId;
            if (!Guid.TryParse(id, out productId))
                return BadRequest();
            var responce = await productService.GetByIdAsync(productId);
            if (responce.StatusCode == HttpStatusCode.OK)
                return Json(mapper.Map<ProductViewModel>(responce.Data));
            else
                return StatusCode((int)responce.StatusCode, responce.Description);
        }

        // POST: Products
        [HttpPost("Product/")]
        public async Task<ActionResult> Create([FromBody]ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                ProductDTO product = mapper.Map<ProductDTO>(model);
                var responce = await productService.CreateAsync(product);
                if (responce.StatusCode == HttpStatusCode.OK)
                    return Json(mapper.Map<ProductViewModel>(responce.Data));
                else
                    return StatusCode((int)responce.StatusCode, responce.Description);
            }
            else
            {
                ModelState.AddModelError("", "BadRequest");
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

        }

        // PUT: Products/id
        [HttpPut("Product/{id}")]
        public async Task<ActionResult> Edit([FromRoute]string id,[FromBody]ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid productId;
                if (!Guid.TryParse(id, out productId))
                    return BadRequest();
                model.Id = productId;
                ProductDTO product = mapper.Map<ProductDTO>(model);
                var responce = await productService.UpdateAsync(product);
                if (responce.StatusCode == HttpStatusCode.OK)
                    return Ok(responce.Data);
                else
                    return StatusCode((int)responce.StatusCode, responce.Description);
            }
            else
            {
                ModelState.AddModelError("", "BadRequest");
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        // Delete: Products/id
        [HttpDelete("Product/{id}")]
        public async Task<ActionResult> Delete([FromRoute]string id)
        {
            Guid productId;
            if (!Guid.TryParse(id, out productId))
                return BadRequest();
            var responce = await productService.DeleteAsync(productId);
            if (responce.StatusCode == HttpStatusCode.OK)
                return Ok();
            else
                return StatusCode((int)responce.StatusCode, responce.Description);
        }
    }
}
