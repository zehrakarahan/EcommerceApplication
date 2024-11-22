using AutoMapper;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Persistance.Mapper.CouponMapping
{
    public class CouponMapping:Profile
    {
        public CouponMapping()
        {

            CreateMap<CouponMapping, Coupons>().ReverseMap();



        }
    }
}
