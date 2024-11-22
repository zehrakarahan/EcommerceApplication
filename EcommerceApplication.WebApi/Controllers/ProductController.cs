using AutoMapper;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Application.Response;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public IMapper _mapper;
        public ProductController(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var products = await _productRepository.GetAllListRelation();
           
            return Ok(products);
        }
        [HttpGet("GetAllProductWithSelect")]
        public async Task<IActionResult> GetAllProductWithSelect()
        {
            var products = await _productRepository.GetAllAsync();
            var result = products.Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.ProductName
            }).ToList();

            return Ok(result);
        }
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductRequest product)
        {
            var productData = new Products();
            _mapper.Map(product, productData);
            await _productRepository.AddAsync(productData);
            return Ok(productData);
        }

 
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Products product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            await _productRepository.UpdateAsync(product);
            return NoContent();
        }


        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("Sales/{id}")]
        public async Task<ActionResult<int>> GetSalesCount(int id)
        {
            var salesCount = await _productRepository.GetProductStockAsync(id);
            return Ok(salesCount);
        }
    }
}
