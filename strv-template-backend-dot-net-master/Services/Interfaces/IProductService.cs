using System.Collections.Generic;
using System.Threading.Tasks;
using Database.ViewModels;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetailViewModel>> GetProducts();
        Task<ProductDetailViewModel> GetProduct(int id);
        Task AddProduct(AddProductViewModel product);
        Task DeleteProduct(int id);
        Task UpdateProduct(UpdateProductViewModel product);
    }
}
