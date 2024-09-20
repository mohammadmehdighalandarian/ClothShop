using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothShop.DataLayer.Entities.Order;
using ClothShop.DataLayer.Entities.Product.ProductDetails;

namespace ClothShop.DataLayer.Entities.Product;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string ProductImageName { get; set; }

    [Display(Name = "عنوان محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string ProductTitle { get; set; }

    [Display(Name = "شرح محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ProductDescription { get; set; }

    [Display(Name = "قیمت محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int ProductPrice { get; set; }

    [Required]
    public int GroupId { get; set; }
    public int? SubGroupId { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public string Tags { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    #region Relations

    [ForeignKey("GroupId")]
    public ProductGroup ProductGroup { get; set; }

    [ForeignKey("SubGroupId")]
    public ProductGroup ProductSubGroup { get; set; }

    public List<Image> Images { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public List<ProductSize> ProductSizes { get; set; }
    public List<ProductMaterial> ProductMaterials { get; set; }
    public List<ProductComment> ProductComments { get; set; }
    public List<UserProduct> UserProducts { get; set; }
    public List<ProductUseType> ProductUseTypes { get; set; }

    #endregion
}