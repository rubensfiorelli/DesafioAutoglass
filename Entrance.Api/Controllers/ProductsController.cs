using Core.Application.DTOs;
using Core.Application.Results;
using Core.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Entrance.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;

        [HttpGet("api/v1/{productId}")]
        public async Task<IActionResult> GetId(int productId)
        {
            try
            {
                var existing = await _productService.GetId(productId);

                return Ok(existing);

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("api/v1/products/{skip:int}/{take:int}")]
        public IActionResult Get()
        {
            try
            {
                var list = _productService.GetAll();

                return Ok(new { data = list });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/v1/products")]
        public async Task<IActionResult> Create(ProductRequest model)
        {
            if ( await _productService.Add(model))       
                return CreatedAtAction(nameof(Create), model);           

            return BadRequest(new Result(400, $"Error", false, model));

        }

        [HttpPut("products/{productId:int}")]
        public async Task<IActionResult> Update(int productId, ProductRequest model)
        {
            try
            {
                var existing = await _productService.GetId(productId);

                await _productService.Update(productId, model);

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("products/{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {

            try
            {
                var existing = await _productService.Delete(productId);
                return NoContent();

            }
            catch (DbException)
            {
                return StatusCode(500);
            }

        }
    }
}
