using EcommerceApplication.Application.Response;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Services
{
     public interface ISalesRepository : IRepository<Sales>
    {
        Task<List<Sales>> GetSalesByDateAsync(DateTime startDate, DateTime endDate);

        Task<List<TopSalesProducts>> GetTopSellingProducts();

        Task<List<TopPreferCategories>> GetTopCategories();

        Task<List<GetDailySales>> GetDailySales();

        Task<List<GetLowStockProducts>> GetLowStockProductsAsync();

        Task<List<Sales>> GetAllSalesWithDummyData();


        Task<List<Sales>> GetAllSales();

    }
}
