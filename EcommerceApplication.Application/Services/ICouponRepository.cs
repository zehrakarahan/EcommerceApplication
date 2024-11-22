using EcommerceApplication.Application.Request;
using EcommerceApplication.Application.Response;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Services
{
    public interface ICouponRepository :IRepository<Coupons>
    {
        Task<CouponRequest> CreateCouponMultiData(CouponRequest couponRequest);

        Task<List<CouponResponse>> GetAllListRelation();
    }
}
