using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/products")]
    public class ProductsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetailViewModel>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products.Any())
            {
                return Ok(products);
            }
            else
            {
                _logger.LogError($"{@Request.Method} {@Request.Path} - No products found");
                return Ok(products);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDetailViewModel>>> Get(int id)
        {
            try
            {
                return Ok(await _productService.GetProduct(id));
            }
            catch (NullReferenceException)
            {
                _logger.LogWarning($"{@Request.Method} {@Request.Path} - Product {id} not found");
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return Accepted(); // TODO: Maybe change this to 204? but can't actually confirm deletion
            }
            catch(DbUpdateConcurrencyException) // TODO: DAL excpetion handling in controller, maybe remove?
            {
                return Accepted();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AddProductViewModel data)
        {
            try
            {
                if (ModelState.IsValid) //TODO: fix this, it currently doesn't check if a key is missing, only if its data is invalid
                                        //e.g. {"name": "A cool new product","productCategoryId": null} errors on validation
                                        //but {"name": "A cool new product"} throws a DbUpdateException
                {
                    await _productService.AddProduct(data);
                    return Accepted();
                }
                else
                {
                    _logger.LogError($"{@Request.Method} {@Request.Path} Request Failed: invalid data:\n {data}");
                    return BadRequest(new { error = "Invalid data" });
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{@Request.Method} {@Request.Path} PostgresException: {ex.Message}");
                return StatusCode(500, $"An error has occured while creating this product");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]int id, [FromBody]UpdateProductViewModel data)
        {
            try
            {
                if (ModelState.IsValid) //TODO: fix this, it currently doesn't check if a key is missing, only if its data is invalid
                                        //e.g. {"name": "A cool new product","productCategoryId": null} errors on validation
                                        //but {"name": "A cool new product"} throws a DbUpdateException
                {
                    data.Id = id;
                    await _productService.UpdateProduct(data);
                    return Accepted();
                }
                else
                {
                    _logger.LogError($"{@Request.Method} {@Request.Path} Request Failed: invalid data:\n {data}");
                    return BadRequest(new { error = "Invalid data" });
                }
            }

            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning($"{@Request.Method} {@Request.Path} - Product {id} not found");
                return NotFound();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{@Request.Method} {@Request.Path} PostgresException: {ex.Message}");
                return StatusCode(500, $"An error has occured while creating this product");
            }
        }
    }
}
