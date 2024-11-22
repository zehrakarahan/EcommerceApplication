using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Request
{
    public class ProductRequest
    {
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int ProductCategoryId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fiyat negatif olamaz.")]
        public decimal ProductPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Miktar negatif olamaz.")]
        public decimal ProductQuantity { get; set; }
    }
}
