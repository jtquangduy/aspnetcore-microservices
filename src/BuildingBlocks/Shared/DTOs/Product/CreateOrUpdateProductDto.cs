using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product;

public class CreateOrUpdateProductDto
{
    [Required]
    [MaxLength(250,ErrorMessage = "Maxium length for Product Name is 250 characters.")]
    public string Name { get; set; }

    [MaxLength(255,ErrorMessage = "Maxium length for Product Name is 255 characters.")]
    public string Summary { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
}