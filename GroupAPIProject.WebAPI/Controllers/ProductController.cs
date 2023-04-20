using GroupAPIProject.Models.Product;
using GroupAPIProject.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GroupAPIProject.WebAPI.Controllers
{
    [Authorize("Roles=AdminEntity")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _productService.CreateProductAsync(model))
            {
                return Ok("Product Was Created");
            }
            return BadRequest("Product Creation Faield");
        }
    }
}
