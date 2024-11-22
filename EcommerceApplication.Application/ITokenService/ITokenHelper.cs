using EcommerceApplication.Application.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.ITokenService
{
    namespace EcommerceApplication.Application.ITokenService
    {
        public interface ITokenHelper
        {
            string CreateToken(TokenUser user);

        }
    }

}
