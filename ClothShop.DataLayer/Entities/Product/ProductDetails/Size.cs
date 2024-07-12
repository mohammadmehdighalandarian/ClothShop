using System.ComponentModel.DataAnnotations;

namespace ClothShop.DataLayer.Entities.Product.ProductDetails;

public class Size
{
    [Key]
    public int SizeId { get; set; }
    public string SizeNO { get; set; }

    #region Relations

    public List<ProductSize> ProductSizes { get; set; }

    #endregion
}