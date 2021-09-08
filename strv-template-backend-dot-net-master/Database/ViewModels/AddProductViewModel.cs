using System;
using System.ComponentModel.DataAnnotations;

namespace Database.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ProductCategoryId { get; set; }

        public AddProductViewModel()
        {
        }
    }
}
