using EcommerceApplication.Application.DtoModel;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Application.Utils;
using EcommerceApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Services
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {

        Task<Result<AccessToken>> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        Task<Result<string>> CreateUserAsync();
        Task<Result<NotImplementedException>> UpdateUserAsync(CreateUserRequest request);
        Task<Result<NotImplementedException>> DeleteUserAsync(string id);
    }
}
