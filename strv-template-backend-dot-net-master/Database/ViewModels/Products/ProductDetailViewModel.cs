using System;
using Database.ViewModels.Products;

namespace Database.ViewModels
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductCategoryViewModel ProductCategory { get; set; }

        public ProductDetailViewModel()
        {
        }
    }
}
