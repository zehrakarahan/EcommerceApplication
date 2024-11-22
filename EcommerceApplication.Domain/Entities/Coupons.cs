using EcommerceApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcommerceApplication.Domain.Common.AppEnums;

namespace EcommerceApplication.Domain.Entities
{
    public class Coupons:BaseEntity
    {
        public string CouponName { get; set; }

        public string? CouponCode { get; set; }


        public DisCountType DiscountType { get; set; }

        public decimal? DiscountValue { get; set; }

        public DateTime StartDateTime { get; set; } 

        public DateTime ExpiryDateTime { get; set; }



        public ICollection<CouponCategory> CouponCategories { get; set; } = new List<CouponCategory>();
        public ICollection<CouponProduct> CouponProducts { get; set; } = new List<CouponProduct>();



    }
    public class CouponCategory
    {
        public int CouponId { get; set; }
        public Coupons Coupon { get; set; }

        public int CategoryId { get; set; }
        public virtual Categories Category { get; set; }
    }

    public class CouponProduct
    {
        public int CouponId { get; set; }
        public Coupons Coupon { get; set; }

        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
    }
}
