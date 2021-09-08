using System.Collections.Generic;
using System.Threading.Tasks;
using Database.ViewModels;
using Database.ViewModels.Products;

namespace Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryViewModel>> GetProductCategories();
        Task<ProductCategoryViewModel> GetProductCategory(int id);
        Task AddProductCategory(AddProductCategoryViewModel productCategory);
        Task DeleteProductCategory(int id);
        Task UpdateProductCategory(ProductCategoryViewModel productCategory);
    }
}