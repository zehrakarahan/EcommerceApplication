using EcommerceApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Persistance.Context
{
    public  class EcommerceContext : IdentityDbContext<ApplicationUser>
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Coupons> Coupons { get; set; }

        public DbSet<Sales> Sales { get; set; } 

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Categories)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        

            modelBuilder.Entity<CouponCategory>()
      .HasKey(cc => new { cc.CouponId, cc.CategoryId }); // Composite key

            modelBuilder.Entity<CouponCategory>()
                .HasOne(cc => cc.Coupon)
                .WithMany(c => c.CouponCategories)
                .HasForeignKey(cc => cc.CouponId);

            modelBuilder.Entity<CouponCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.CouponCategories)
                .HasForeignKey(cc => cc.CategoryId);

            // CouponProduct (Coupon ve Product arasındaki ilişki)
            modelBuilder.Entity<CouponProduct>()
                .HasKey(cp => new { cp.CouponId, cp.ProductId }); // Composite key

            modelBuilder.Entity<CouponProduct>()
                .HasOne(cp => cp.Coupon)
                .WithMany(c => c.CouponProducts)
                .HasForeignKey(cp => cp.CouponId);

            modelBuilder.Entity<CouponProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CouponProducts)
                .HasForeignKey(cp => cp.ProductId);
        }

    }
}
