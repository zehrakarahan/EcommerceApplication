using EcommerceApplication.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Domain.Entities
{
    public class Categories:BaseEntity
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }
        public string CategoryName {  get; set; }

        public string CategoryDescription { get; set; }



        public ICollection<Products> Products { get; set; }

        public ICollection<CouponCategory> CouponCategories { get; set; } = new List<CouponCategory>();


    }
}
