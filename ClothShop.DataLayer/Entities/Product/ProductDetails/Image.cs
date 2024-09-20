using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.DataLayer.Entities.Product.ProductDetails;

public class Image
{
    [Key]
    public int id { get; set; }
    public int ProductId { get; set; }
    public string ImageName { get; set; }

    #region Relation

    [ForeignKey("ProductId")]
    public Product Products { get; set; }

    #endregion

}