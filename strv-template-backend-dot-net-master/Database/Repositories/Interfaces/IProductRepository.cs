using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Repositories.Base;

namespace Database.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Entities.Product>
    {
        Task<IEnumerable<Entities.Product>> GetProductsWithCategories();
    }
}