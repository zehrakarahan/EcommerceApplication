using EcommerceApplication.Persistance.Context;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerceApplication.Application.Response;
using Microsoft.AspNet.Identity;
using Bogus;
using static EcommerceApplication.Domain.Common.AppEnums;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

namespace EcommerceApplication.Persistance.Repositories
{
    public class SalesRepository : Repository<Sales>, ISalesRepository
    {
        public EcommerceContext _context;

       


        public SalesRepository(EcommerceContext context) : base(context)
        {
            _context = context;
           
        }
        public async Task<List<Sales>> GetSalesByDateAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Sales.Include(s => s.Products).Include(s => s.Coupon)
                                       .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                                       .ToListAsync();
        }

        public async Task<List<TopSalesProducts>> GetTopSellingProducts()
        {
            var topSellingProducts = await _context.Sales
               .Include(s => s.Products)  
               .GroupBy(s => s.ProductID)  
               .Select(group => new TopSalesProducts
               {
                   ProductName = group.FirstOrDefault().Products.ProductName, 
                   TotalSales = group.Sum(s => s.ProductAmount), 
                   TotalSalesTotal = group.Sum(s => s.ProductPrice * s.ProductAmount) 
               })
               .OrderByDescending(x => x.TotalSales)  
               .Take(5)  
               .ToListAsync();

            return topSellingProducts;

        }
        public async Task<List<TopPreferCategories>> GetTopCategories()
        {
            var topCategories = await _context.Sales
                .Include(s => s.Products) 
                .ThenInclude(p => p.Categories) 
                .GroupBy(s => s.Products.ProductCategoryId)
                .Select(group => new  TopPreferCategories
                {
                    CategoryId = group.Key, 
                    CategoryName = group.FirstOrDefault().Products.Categories.CategoryName, 
                    TotalSales = group.Sum(s => s.ProductAmount) 
                })
                .OrderByDescending(x => x.TotalSales) 
                .Take(5) 
                .ToListAsync();

            return topCategories;
        }

        public async Task<List<GetDailySales>> GetDailySales()
        {
            var dailySales = await _context.Sales
                .Where(s => s.SaleDate >= DateTime.Today) 
                .GroupBy(s => s.SaleDate)
                .Select(group => new GetDailySales
                {
                    Date = group.Key,
                    TotalSales = group.Sum(s => s.TotalPrice) 
                })
                .ToListAsync();

            return dailySales;
        }

        public async Task<List<GetLowStockProducts>> GetLowStockProductsAsync()
        {
            return await _context.Products
                .Where(p => p.ProductQuantity < 10) 
                .Select(p => new GetLowStockProducts
                {
                    ProductName = p.ProductName, 
                    ProductQuantity = p.ProductQuantity,
                    ProductPrice = p.ProductPrice 
                })
                .ToListAsync();
        }

        public async Task<List<Sales>> GetAllSalesWithDummyData()
        {

                var coupons = _context.Coupons.Where(x => x.StartDateTime <= DateTime.Now && x.ExpiryDateTime >= DateTime.Now &&
           x.IsActive && x.IsDeleted != true).
           Include(x => x.CouponProducts).Include(k => k.CouponCategories);

                var products = _context.Products.ToList();

                var faker = new Faker<Sales>()
                .RuleFor(u => u.Id, f => f.IndexFaker + 1)
                .RuleFor(s => s.ProductID, f => f.PickRandom(products).Id)
                .RuleFor(s => s.ProductAmount, f => f.Random.Int(1, 10))
                .RuleFor(s => s.ProductPrice, (f, s) =>
                {
                    var product = products.FirstOrDefault(p => p.Id == s.ProductID);
                    return product != null ? product.ProductPrice : 0;
                })
                .RuleFor(s => s.SaleDate, f => f.Date.Recent())
                .RuleFor(s => s.SaleStatus, f => f.PickRandom(new[] { "Completed", "Pending", "Cancelled" }))
                .RuleFor(s => s.Coupon, (f, s) =>
                {
                    return coupons.FirstOrDefault(c =>
                        c.CouponProducts.Any(cp => cp.ProductId == s.ProductID));
                })
                .RuleFor(s => s.CouponId, (f, s) => s.Coupon?.Id)
                 .RuleFor(s => s.Products, (f, s) =>
                 {
                   return products.FirstOrDefault(c => c.Id == s.ProductID);
                     
                 })
                .RuleFor(s => s.TotalPrice, (f, s) =>
                {
                    decimal totalPrice = s.ProductAmount * s.ProductPrice;

                    if (s.Coupon != null)
                    {
                        if (s.Coupon.DiscountType == DisCountType.PersantageRace && s.Coupon.DiscountValue.HasValue)
                        {
                            totalPrice -= (totalPrice * s.Coupon.DiscountValue.Value);
                        }
                        else if (s.Coupon.DiscountType == DisCountType.FixedAmount && s.Coupon.DiscountValue.HasValue)
                        {
                            totalPrice -= s.Coupon.DiscountValue.Value;
                        }
                    }

                    return totalPrice > 0 ? totalPrice : 0;
                })
                
                .FinishWith((f, s) =>
                {
                    // stok sayılarını düşürmek iiçin yazdıldı bu kod bilginizie 
                    var product = products.FirstOrDefault(p => p.Id == s.ProductID);
                    if (product != null)
                    {
                        if (product.ProductQuantity >= s.ProductAmount)
                        {
                            product.ProductQuantity -= s.ProductAmount;
                        }
                        else
                        {
                            throw new Exception($"Yeterli stok yok. Ürün ID: {product.Id}");
                        }
                    }
                });
                var sales = faker.Generate(3);
                //stok miktrın günclemek içim yazıldı. 
                _context.Products.UpdateRange(products);
                _context.Sales.AddRange(sales);
                _context.SaveChanges();
          

                return sales;
           
        }

        public async Task<List<Sales>> GetAllSales()
        {
            var dataList =await _context.Sales.Where(x => x.IsActive && x.IsDeleted != true).Include(x => x.Coupon).Include(x => x.Products).ToListAsync();

           return dataList;
        }
    }
}
