using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Extensions
{
    public static class ClaimExtension
    {

        public static void AddId(this ICollection<Claim> claims, string id) { claims.Add(new Claim("Id", id)); }
        public static void AddUserName(this ICollection<Claim> claims, string firstName) { claims.Add(new Claim("FirstName", firstName)); }

        public static void AddEmail(this ICollection<Claim> claims, string email) { claims.Add(new Claim("Email", email)); }

        public static void AddRoles(this ICollection<Claim> claims, string roleName)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));
        }
    }
}
