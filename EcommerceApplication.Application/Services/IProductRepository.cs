using EcommerceApplication.Application.Response;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Services
{
    public interface IProductRepository:IRepository<Products>
    {
        Task<decimal> GetProductStockAsync(int productId);

        Task<List<ProductList>> GetAllListRelation();
    }
}
