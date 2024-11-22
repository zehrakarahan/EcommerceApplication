using AutoMapper;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Application.Response;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Persistance.Mapper.Sales
{
    public class SalesMapping :Profile
    {
        public SalesMapping()
        {

            CreateMap<SalesResponse, EcommerceApplication.Domain.Entities.Sales> ().ReverseMap()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products != null ? src.Products.ProductName : "Unknown Product"))
            .ForMember(dest => dest.CouponName, opt => opt.MapFrom(src => src.Coupon != null ? src.Coupon.CouponName : null)); ;

        }
    }
}
