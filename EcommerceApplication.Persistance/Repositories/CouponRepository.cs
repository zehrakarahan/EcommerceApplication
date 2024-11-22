using EcommerceApplication.Persistance.Context;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceApplication.Application.Request;
using Microsoft.EntityFrameworkCore;
using EcommerceApplication.Application.Response;

namespace EcommerceApplication.Persistance.Repositories
{
    public class CouponRepository : Repository<Coupons>, ICouponRepository
    {
        private readonly EcommerceContext _context;
        public CouponRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CouponRequest> CreateCouponMultiData(CouponRequest couponRequest)
        {
                var coupon = new Coupons
                {
                    CouponName = couponRequest.CouponName,
                    CouponCode = couponRequest.CouponCode,
                    DiscountValue = couponRequest.DiscountValue,
                    ExpiryDateTime = couponRequest.ExpiryDateTime,
                    StartDateTime = couponRequest.StartDateTime,
                    DiscountType = couponRequest.DiscountType,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                    
                };


                if (couponRequest.CategoriesIdList!.Count() > 0)
                {
                    foreach (var item in couponRequest.CategoriesIdList!)
                    {
                        coupon.CouponCategories.Add(new CouponCategory
                        {
                            CategoryId = item,
                            Coupon = coupon
                        });
                    }
                }
                if (couponRequest.ProductIdList!.Count() > 0)
                {
                    foreach (var item in couponRequest.ProductIdList!)
                    {
                        coupon.CouponProducts.Add(new CouponProduct
                        {
                            ProductId = item,
                            Coupon = coupon
                        });
                    }
                }
                await _context.Coupons.AddAsync(coupon);
                await _context.SaveChangesAsync();
            
            
         

            return couponRequest;
        }

        public async Task<List<CouponResponse>> GetAllListRelation()
        {
            var data = await _context.Coupons
          .Include(p => p.CouponCategories)
              .ThenInclude(cc => cc.Category)
          .Include(k => k.CouponProducts)
          .ThenInclude(dd => dd.Product)
          .Select(c => new CouponResponse
          {

              CouponName = c.CouponName,
              CouponCode = c.CouponCode,
              DiscountType = c.DiscountType,
              DiscountValue = c.DiscountValue,
              ExpiryDateTime = c.ExpiryDateTime,
              StartDateTime = c.StartDateTime,
              Id = c.Id,
              CouponCategories = c.CouponCategories.Select(cc => new CategoryDto
              {
                  CategoryId = cc.CategoryId,
                  CategoryName = cc.Category.CategoryName
              }).ToList(),

              CouponProducts = c.CouponProducts.Select(cp => new ProductDto
              {
                  ProductId = cp.ProductId,
                  ProductName = cp.Product.ProductName
              }).ToList()
          })
          .ToListAsync();


            return data;
        }
    }
}
