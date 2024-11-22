using AutoMapper;
using EcommerceApplication.Application.Request;
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
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public IMapper _mapper;

        public CouponController(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }


        [HttpGet("GetAllCoupons")]
        public async Task<IActionResult> GetAllCoupons()
        {
            var coupons = await _couponRepository.GetAllListRelation();
            return Ok(coupons);
        }

        [HttpPost("CreateCoupon")]
        public async Task<IActionResult> CreateCoupon(CouponRequest couponRequest)
        {
            if (couponRequest == null)
            {
                return BadRequest("Coupon is null");
            }


            var createdCoupon = await _couponRepository.CreateCouponMultiData(couponRequest);
            return Ok(createdCoupon);
        }

        [HttpGet("GetCouponById/{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var coupon = await _couponRepository.GetByIdAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        [HttpPut("UpdateCoupon/{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] Coupons coupon)
        {
            if (id != coupon.Id)
            {
                return BadRequest("Coupon ID mismatch");
            }

            var existingCoupon = await _couponRepository.GetByIdAsync(id);
            if (existingCoupon == null)
            {
                return NotFound();
            }

            var updatedCoupon = await _couponRepository.UpdateAsync(coupon);
            return Ok(updatedCoupon);
        }

        [HttpDelete("DeleteCategories/{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var result = await _couponRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
