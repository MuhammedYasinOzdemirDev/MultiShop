using System.ComponentModel.DataAnnotations;

namespace MultiShop.Catalog.Dtos.Category;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Name is required")]
    public string CategoryName { get; set; }
}