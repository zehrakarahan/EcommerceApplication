using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Response
{
    public class ProductList
    {

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int ProductCategoryId { get; set; }

     
        public decimal ProductPrice { get; set; }

       
        public decimal ProductQuantity { get; set; }

        public string CategoriName { get; set; }
    }
}
