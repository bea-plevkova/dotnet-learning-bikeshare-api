using System.ComponentModel.DataAnnotations;

namespace Database.ViewModels
{
    public class AddProductCategoryViewModel
    {
        [Required] [MaxLength(255)] public string Name { get; set; }
    }
}