using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcommerceApplication.Domain.Common.AppEnums;

namespace EcommerceApplication.Application.Response
{
    public class CouponResponse
    {
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        public DisCountType DiscountType { get; set; }
        public decimal? DiscountValue { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public int Id { get; set; }
        public List<CategoryDto> CouponCategories { get; set; }
        public List<ProductDto> CouponProducts { get; set; }
    }

    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }

}
