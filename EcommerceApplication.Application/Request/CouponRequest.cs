using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcommerceApplication.Domain.Common.AppEnums;

namespace EcommerceApplication.Application.Request
{
    public class CouponRequest
    {
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        public DisCountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public List<int>? ProductIdList { get; set; } 
        public List<int>? CategoriesIdList { get; set; } 
    }

}
