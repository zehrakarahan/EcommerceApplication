using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Response
{
    public class GetDailySales
    {
        public DateTime Date {  get; set; }

        public decimal TotalSales { get; set; }
    }
}
