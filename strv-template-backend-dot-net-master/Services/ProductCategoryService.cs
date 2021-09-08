using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using Database.Repositories.Interfaces;
using Database.ViewModels;
using Database.ViewModels.Products;
using Services.Interfaces;

namespace Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetProductCategories()
        {
            var productCategories = await _productCategoryRepository.List();
            return productCategories.Select(pc => new ProductCategoryViewModel
            {
                Id = pc.Id,
                Name = pc.Name
            });
        }

        public async Task AddProductCategory(AddProductCategoryViewModel productCategory)
        {
            var productCategoryEntity = new ProductCategory
            {
                Name = productCategory.Name
            };

            await _productCategoryRepository.Insert(productCategoryEntity);
        }

        public Task DeleteProductCategory(int id)
        {
            var ProductCategory = new ProductCategory {Id = id};
            return _productCategoryRepository.Delete(ProductCategory);
        }

        public async Task<ProductCategoryViewModel> GetProductCategory(int id)
        {
            var productCategory = await _productCategoryRepository.GetById(id);
            return new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name
            };
        }

        public async Task UpdateProductCategory(ProductCategoryViewModel productCategory)
        {
            var productCategoryEntity = new ProductCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name
            };

            await _productCategoryRepository.Update(productCategoryEntity);
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetProductCategoriesAsync()
        {
            var productCategory = await _productCategoryRepository.List();
            return productCategory.Select(pC => new ProductCategoryViewModel
            {
                Id = pC.Id,
                Name = pC.Name
            });
        }
    }
}