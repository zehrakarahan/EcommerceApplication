using EcommerceApplication.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Domain.Entities
{
    public  class Products:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int ProductCategoryId { get; set;}

        [Range(0, double.MaxValue, ErrorMessage = "Fiyat negatif olamaz.")]
        public decimal ProductPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Miktar negatif olamaz.")]
        public decimal ProductQuantity { get; set; }




        public Categories Categories { get; set; }


        public ICollection<CouponProduct> CouponProducts { get; set; } = new List<CouponProduct>();



    }
}
