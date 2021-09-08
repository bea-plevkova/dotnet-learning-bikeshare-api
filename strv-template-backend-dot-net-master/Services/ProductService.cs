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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDetailViewModel>> GetProducts()
        {
            var products = await _productRepository.GetProductsWithCategories();
            return products.Select(p => new ProductDetailViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ProductCategory = new ProductCategoryViewModel
                {
                    Id = p.ProductCategory.Id,
                    Name = p.ProductCategory.Name
                }
            });
        }

        public async Task<ProductDetailViewModel> GetProduct(int Id)
        {
            var product = await _productRepository.GetById(Id);
            return new ProductDetailViewModel
            {
                Id = product.Id,
                Name = product.Name
            };
        }

        public async Task AddProduct(AddProductViewModel product)
        {
            var productEntity = new Product
            {
                Name = product.Name,
                ProductCategoryId = product.ProductCategoryId
            };

            await _productRepository.Insert(productEntity);
        }

        public async Task UpdateProduct(UpdateProductViewModel product)
        {
            var productEntity = new Product
            {
                Id = product.Id,
                Name = product.Name,
                ProductCategoryId = product.ProductCategoryId
            };

            await _productRepository.Update(productEntity);
        }

        public Task DeleteProduct(int id)
        {
            var product = new Product {Id = id};
            return _productRepository.Delete(product);
        }
    }
}