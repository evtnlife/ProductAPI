using Microsoft.AspNetCore.Mvc;
using ProductAPI.Domain.Contract.Services;
using ProductAPI.Domain.Requests;
using ProductAPI.Domain.Requests.Errors;
using ProductAPI.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedReturn), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PreconditionFailedReturn), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerErrorReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _service.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha capturada ProductsController::GetProducts");
                return StatusCode(500, new InternalServerErrorReturn("Erro interno no servidor."));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedReturn), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PreconditionFailedReturn), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerErrorReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _service.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound("Produto não encontrado.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha capturada ProductsController::GetProduct");
                return StatusCode(500, new InternalServerErrorReturn("Erro interno no servidor."));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnauthorizedReturn), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PreconditionFailedReturn), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerErrorReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BadRequestReturn("Falha ao criar produto.", ModelState.GetErrors()));
            }

            try
            {
                await _service.AddProductAsync(product);
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha capturada ProductsController::CreateProducts");
                return StatusCode(500, new InternalServerErrorReturn("Erro interno no servidor."));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedReturn), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PreconditionFailedReturn), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerErrorReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            if (id != product.ProductId)
            {
                return BadRequest(new BadRequestReturn("ID do produto não corresponde."));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BadRequestReturn("ID do produto não corresponde.", ModelState.GetErrors()));
            }

            try
            {
                await _service.UpdateProductAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha capturada ProductsController::UpdateProduct");
                return StatusCode(500, new InternalServerErrorReturn("Erro interno no servidor."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedReturn), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PreconditionFailedReturn), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerErrorReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha capturada ProductsController::DeleteProduct");
                return StatusCode(500, new InternalServerErrorReturn("Erro interno no servidor."));
            }
        }
    }
}
