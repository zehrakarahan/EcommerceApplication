using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Utils
{
    public class Result<T>
    {
        public Result() { }

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Result(bool success, T data) : this(success)
        {
            Data = data;
        }

        public Result(bool success, string message, T data) : this(success)
        {
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public string SerializeObject()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string SerializeData()
        {
            if (this.Data != null)
                return JsonConvert.SerializeObject(this.Data);

            return "";
        }
    }
}
