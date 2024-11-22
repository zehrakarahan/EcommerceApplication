using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Utils
{
    public interface IResult
    {
        bool Success { get; set; }
        string? Message { get; set; }
        string? Code { get; set; }
        object? Data { get; set; }
    }
}
