using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Response
{
    public class GetLowStockProducts
    {
        public string ProductName {  get; set; }

        public decimal ProductQuantity {  get; set; }

        public decimal ProductPrice { get; set; }
    }
}
