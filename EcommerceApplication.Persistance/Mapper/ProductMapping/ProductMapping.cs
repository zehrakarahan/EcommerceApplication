using AutoMapper;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Persistance.Mapper.ProductMapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {

            CreateMap<ProductRequest, Products>().ReverseMap();



        }
    }
}
