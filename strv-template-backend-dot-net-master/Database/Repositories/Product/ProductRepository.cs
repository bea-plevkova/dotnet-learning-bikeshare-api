using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Database.Entities;
using Database.Repositories.Base;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Product
{
    public class ProductRepository : BaseRepository<Entities.Product>, IProductRepository
    {
        public ProductRepository(StrvTemplateDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Entities.Product>> GetProductsWithCategories()
        {
            return await _dbContext.Products.Include(p => p.ProductCategory).ToListAsync();
        }
    }
}
