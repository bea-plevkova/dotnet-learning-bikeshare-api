using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using Database.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/productcategories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesController(IProductCategoryService productCategoryService,
            ILogger<ProductsController> logger)
        {
            _productCategoryService = productCategoryService;
            _logger = logger;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            var productCategories = await _productCategoryService.GetProductCategories();
            if (productCategories.Any()) return Ok(productCategories);

            _logger.LogError($"{Request.Method} {Request.Path} - No products found");
            return Ok(productCategories);
        }

        // POST: api/ProductCategories
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(
            [FromBody] AddProductCategoryViewModel productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productCategoryService.AddProductCategory(productCategory);
                    return Accepted();
                }

                _logger.LogError($"{Request.Method} {Request.Path} Request Failed: invalid data:\n {productCategory}");
                return BadRequest(new {error = "Invalid data"});
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{Request.Method} {Request.Path} PostgresException: {ex.Message}");
                return StatusCode(500, "An error has occured while creating this product");
            }
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategory>> DeleteProductCategory(int id)
        {
            try
            {
                await _productCategoryService.DeleteProductCategory(id);
                return Accepted(); // TODO: Maybe change this to 204? but can't actually confirm deletion
            }
            catch (DbUpdateConcurrencyException) // TODO: DAL excpetion handling in controller, maybe remove?
            {
                return Accepted();
            }
        }
    }
}