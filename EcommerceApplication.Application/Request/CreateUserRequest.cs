﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Request
{
    public  class CreateUserRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }    

    }
}
