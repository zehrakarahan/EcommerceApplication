using AutoMapper;
using EcommerceApplication.Application.Response;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Entities;
using EcommerceApplication.Persistance.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;

        public SalesController(ISalesRepository salesRepository,IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        [HttpPost("GetAllDummyAddRange")]
        public async Task<IActionResult> GetAllDummyAddRange()
        {
            var data =await _salesRepository.GetAllSalesWithDummyData();
            return Ok();
        }
       
        [HttpGet("GetAllDummyList")]
        public async Task<IActionResult> GetAllDummyList()
        {
            var sales=await _salesRepository.GetAllSales();
            if (sales == null || !sales.Any())
            {
               sales=await _salesRepository.GetAllSalesWithDummyData();
            }

            var salesResponses = _mapper.Map<List<SalesResponse>>(sales);
            return Ok(salesResponses);
        }

        
        [HttpGet("Getbyid/{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var sale = await _salesRepository.GetByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

       
        [HttpPost("Addsale")]
        public async Task<IActionResult> AddSale([FromBody] Sales sale)
        {
            if (sale == null)
            {
                return BadRequest("Invalid sale data.");
            }

            var saleId = await _salesRepository.AddAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = saleId }, sale);
        }


        [HttpPut("Updatesale/{id}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] Sales sale)
        {
            if (sale == null || sale.Id != id)
            {
                return BadRequest("Sale data is incorrect.");
            }

            var isUpdated = await _salesRepository.UpdateAsyncBool(id,sale);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

  
        [HttpDelete("Deletesale/{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var isDeleted = await _salesRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("Getbydate/{startDate}/{endDate}")]
        public async Task<IActionResult> GetSalesByDate(DateTime startDate, DateTime endDate)
        {
            var sales = await _salesRepository.GetSalesByDateAsync(startDate, endDate);
            if (sales == null || !sales.Any())
            {
                return NotFound();
            }
            return Ok(sales);
        }
        [HttpGet("GetTopSellingProducts")]
        public async Task<IActionResult> GetTopSellingProducts()
        {
            var topSellingProducts =await _salesRepository.GetTopSellingProducts();

            return Ok(topSellingProducts);
        }
        [HttpGet("GetTopCategories")]
        public async Task<IActionResult> GetTopCategories()
        {
            var topCategories =await _salesRepository.GetTopCategories();

            return Ok(topCategories);
        }
        [HttpGet("GetDailySales")]
        public async Task<IActionResult> GetDailySales()
        {
            var dailySales = await _salesRepository.GetDailySales();
            return Ok(dailySales);
        }

        [HttpGet("LowStock")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            var lowStockProducts = await _salesRepository.GetLowStockProductsAsync();

            if (!lowStockProducts.Any())
            {
                return NotFound("No low stock products found.");
            }

            return Ok(lowStockProducts);
        }
    }
}
