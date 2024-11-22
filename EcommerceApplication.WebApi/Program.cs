using EcommerceApplication.Application.ITokenService;
using EcommerceApplication.Application.ITokenService.EcommerceApplication.Application.ITokenService;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Entities;
using EcommerceApplication.Persistance.Context;
using EcommerceApplication.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

// Database configuration (In-Memory for development)
builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Identity configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password policy
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 7;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<EcommerceContext>()
.AddDefaultTokenProviders();

// CORS configuration (Allow all origins for now)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() // Herhangi bir origin'e izin ver
               .AllowAnyMethod() // Herhangi bir HTTP metoduna izin ver
               .AllowAnyHeader(); // Herhangi bir baþlýða izin ver
    });
});

builder.Services.AddControllers();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoriesRepository>();

// Token Helper
builder.Services.AddScoped<ITokenHelper, TokenHelper>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var key = Encoding.ASCII.GetBytes(builder.Configuration["Token:SecretKey"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"]
    };
});

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseHttpsRedirection();  // HTTPS'ye yönlendirme middleware'ini ekliyoruz
app.UseStaticFiles();  // Statik dosyalarýn sunulmasý (e.g., resimler, JS, CSS)

app.UseRouting();  // Routing middleware

app.UseCors("AllowAll");  // CORS middleware'ini burada ekliyoruz

app.UseAuthentication();  // JWT Authentication middleware
app.UseAuthorization();   // Authorization middleware

app.MapControllers();  // API Controller'larýný yönlendirmek

app.Run();
