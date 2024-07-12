using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothShop.DataLayer.Entities.Product;

public class ProductGroup
{
    [Key]
    public int GroupId { get; set; }

    [Display(Name = "عنوان گروه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string GroupTitle { get; set; }

    [Display(Name = "حذف شده ؟")]
    public bool IsDelete { get; set; }

    [Display(Name = "گروه اصلی")]
    public int? ParentId { get; set; }


    #region Relation

    [ForeignKey("ParentId")]
    public List<ProductGroup> ProductGroups { get; set; }

    [InverseProperty("ProductGroup")]
    public List<Product> Products { get; set; }

    [InverseProperty("ProductSubGroup")]
    public List<Product> SubGroup { get; set; }

    #endregion
}