

using EcommerceApplication.Application.DtoModel;
using EcommerceApplication.Application.ITokenService;
using EcommerceApplication.Application.ITokenService.EcommerceApplication.Application.ITokenService;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Application.Utils;
using EcommerceApplication.Domain.Entities;
using EcommerceApplication.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace EcommerceApplication.Persistance.Repositories
{
    public class UserRepository : Repository<ApplicationUser>,IUserRepository
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenHelper _tokenHelper;
        public UserRepository(EcommerceContext context, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
            ITokenHelper tokenHelper) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
        }

        public async Task<Result<AccessToken>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new Result<AccessToken>(false, "User not found");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return new Result<AccessToken>(false, "Invalid password");
            }

            // Token oluşturma
            var token = _tokenHelper.CreateToken(new TokenUser
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                RoleName = "Admin" // Kullanıcının rolü
            });
           

            return new Result<AccessToken>(true, "Login successful", new AccessToken
            {
                Expiration = DateTime.UtcNow.AddMinutes(15), // Token geçerlilik süresi
                Token = token,
                UserName = user.UserName
            });
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Result<string>> CreateUserAsync()
        {
            try
            {
                var result = await _userManager.CreateAsync(new ApplicationUser
                {
                    Email = "zehrakarahan96@gmail.com",
                    UserName = "admin"
                }, "password123");
                if (result.Succeeded)
                    return new Result<string>(true, "User create success");
                if (result.Errors.Count() > 0)
                {
                    var errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += errors + error.Description;
                    }
                    return new Result<string>(false, errors);
                }
            }
            catch(Exception ex)
            {
                throw new NotImplementedException();
            }
         
            return new Result<string>(true,"");    
        }

        public string DeleteUserAsync(string Id)
        {
            throw new NotImplementedException();
        }

    

        Task<Result<NotImplementedException>> IUserRepository.UpdateUserAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        Task<Result<NotImplementedException>> IUserRepository.DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }
    }

}
