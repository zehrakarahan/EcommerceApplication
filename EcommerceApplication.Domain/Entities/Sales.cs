using EcommerceApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Domain.Entities
{
    public class Sales:BaseEntity
    {
        public int ProductID { get; set; }

        public Products Products { get; set; }

        public int ProductAmount { get; set; }  


        public decimal ProductPrice {  get; set; }

        public decimal TotalPrice {  get; set; }


        public DateTime SaleDate { get; set; }  

        public int? CouponId { get; set; }  
        public Coupons Coupon { get; set; }  

      

        public string SaleStatus { get; set; }
    }
}
