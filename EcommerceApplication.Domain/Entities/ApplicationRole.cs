using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Domain.Entities
{
    public class ApplicationRole:IdentityRole<string>
    {
        public string Id { get ; set; } 
    }
}
