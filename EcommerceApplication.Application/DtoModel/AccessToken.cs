using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.DtoModel
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public string UserName { get; set; }

        public string RoleName { get;set; }

    }
}
