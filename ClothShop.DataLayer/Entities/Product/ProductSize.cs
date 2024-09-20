using System.ComponentModel.DataAnnotations;
using ClothShop.DataLayer.Entities.Product.ProductDetails;

namespace ClothShop.DataLayer.Entities.Product;

public class ProductSize
{
    [Key]
    public int ProductSizeId { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int Count { get; set; }

    #region Relations

    public Product Product { get; set; }
    public Size Size { get; set; }

    #endregion
}