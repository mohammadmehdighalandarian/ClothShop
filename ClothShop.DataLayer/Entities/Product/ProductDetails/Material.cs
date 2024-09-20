using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.DataLayer.Entities.Product.ProductDetails;

public class Material
{
    [Key]
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
    public bool IsActive { get; set; }


    #region Relations

    public List<ProductMaterial> ProductMaterials { get; set; }

    #endregion
}