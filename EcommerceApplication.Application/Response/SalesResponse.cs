using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Response
{
    public class SalesResponse
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; } 

        public int ProductAmount { get; set; }


        public decimal ProductPrice { get; set; }

        public decimal TotalPrice { get; set; }


        public DateTime SaleDate { get; set; }

        public string CouponName { get; set; }

        public string SaleStatus { get; set; }
    }
}
