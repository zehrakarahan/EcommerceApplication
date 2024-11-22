
using AutoMapper;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Application.Response;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Entities;
using EcommerceApplication.Persistance.Mapper.CategoriesMapping;
using EcommerceApplication.Persistance.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace EcommerceApplication.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        public ICategoryRepository _categoryRepository;
        public IMapper  _mapper;
        public CategoriesController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }

        [HttpPost("SaveCategories")]
        public async Task<IActionResult> SaveCategories([FromBody]CategoriesRequest request)
        {
            var categories = new Categories();
            var mapping = _mapper.Map( request, categories);
            var result =await _categoryRepository.AddAsync(mapping);
            
            return Ok(result);
        }
        [HttpGet("GetAllCategories")]
     
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryRepository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("GetAllCategoriesWithSelect")]
        public async Task<IActionResult> GetAllCategoriesWithSelect()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var result = categories.Select(category => new CategoryResponse
            {
                Id = category.Id,
                Name = category.CategoryName
            }).ToList();

            return Ok(result);
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories(int Id)
        {
            var result = await _categoryRepository.GetByIdAsync(Id);
            return Ok(result);
        }
        [HttpPut("UpdateCategories")]
        public async Task<IActionResult> GetCategory(int id,CategoriesRequest categoriesRequest)
        {
            if (id > 0)
            {
                var result =await _categoryRepository.GetByIdAsync(id);
                if (result.Id > 0)
                {
                    var mapping = _mapper.Map(result, categoriesRequest);
                    return Ok(mapping);
                }
                return BadRequest("categories dont found");
            }
                
            else return BadRequest("enter valid number");
        }
        [HttpGet("DeleteCategories")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                var result =await _categoryRepository.DeleteAsync(id);

                if (result) return Ok("Data deleted successfully.");
                else return BadRequest("The record could not be found or deleted.");
            }
              
            else return BadRequest("enter valid number");

        }


    }
}
