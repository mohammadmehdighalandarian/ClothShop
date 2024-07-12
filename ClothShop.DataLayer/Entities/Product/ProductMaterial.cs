using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothShop.DataLayer.Entities.Product.ProductDetails;

namespace ClothShop.DataLayer.Entities.Product;

public class ProductMaterial
{
    [Key]
    public int ProductMaterialId { get; set; }

    public int ProductId { get; set; }

    public int MaterialId { get; set; }


    #region Relations

    [ForeignKey("MaterialId")]
    public Material Material { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    #endregion

}