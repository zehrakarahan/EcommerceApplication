using EcommerceApplication.Persistance.Context;
using EcommerceApplication.Application.Services;
using EcommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Persistance.Repositories
{
    public class CategoriesRepository : Repository<Categories>, ICategoryRepository
    {
        public CategoriesRepository(EcommerceContext appDbContext) : base(appDbContext)
        {
        }
    }
}
