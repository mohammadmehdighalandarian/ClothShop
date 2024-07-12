using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothShop.DataLayer.Entities.Product.ProductDetails;

namespace ClothShop.DataLayer.Entities.Product;

public class ProductUseType
{
    [Key]
    public int ProductUseTypeId { get; set; }

    public int ProductId { get; set; }

    public int UseTypeId { get; set; }


    #region Relations

    [ForeignKey("UseTypeId")]
    public UseType UseType { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    #endregion
}