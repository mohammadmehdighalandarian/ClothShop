using System.ComponentModel.DataAnnotations;

namespace ClothShop.DataLayer.Entities.Product.ProductDetails;

public class UseType
{
    [Key]
    public int TypeId { get; set; }
    [Required]
    public string TypeName { get; set; }

    #region Relations

    public List<ProductUseType> ProductUseTypes { get; set; }

    #endregion
}