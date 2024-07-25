
using System.ComponentModel.DataAnnotations;

namespace ClothShop.Core.DTOs.Product;

public class ShowProductForAdmin
{
    public int ProductId { get; set; }
    public string ProductImageName { get; set; }
    public string ProductTitle { get; set; }
    public string MainGroup { get; set; }
    public string? SubGroup { get; set; }
    public bool IsActive { get; set; }
}