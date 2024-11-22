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

namespace EcommerceApplication.Persistance.Repositories
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        private readonly EcommerceContext _context;

        public ProductRepository(EcommerceContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<List<ProductList>> GetAllListRelation()
        {
            var data = await _context.Products
               .Include(p => p.Categories)
               .Select(p => new ProductList
               {
                   ProductName = p.ProductName,
                   ProductDescription = p.ProductDescription,
                   ProductCategoryId = p.ProductCategoryId, // Assuming `CategoryId` is the foreign key
                   ProductPrice = p.ProductPrice,
                   ProductQuantity = p.ProductQuantity,
                   CategoriName = p.Categories.CategoryName // Assuming Categories has a `CategoryName` property
               })
              .ToListAsync();
             
            return data;

        }

        public async Task<decimal> GetProductStockAsync(int productId)
        {
           
            var product = await _context.Products
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(p => p.Id == productId);

            return product?.ProductQuantity ?? 0;
        }


    }
}
