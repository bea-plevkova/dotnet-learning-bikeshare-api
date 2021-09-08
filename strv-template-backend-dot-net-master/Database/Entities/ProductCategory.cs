using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Repositories.Base;

namespace Database.Entities
{
    public class ProductCategory : IBaseEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
        }
    }
}
