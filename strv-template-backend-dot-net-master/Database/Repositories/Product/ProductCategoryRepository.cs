using Database.Entities;
using Database.Repositories.Base;
using Database.Repositories.Interfaces;

namespace Database.Repositories.Product
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(StrvTemplateDbContext context) : base(context)
        {
        }
    }
}