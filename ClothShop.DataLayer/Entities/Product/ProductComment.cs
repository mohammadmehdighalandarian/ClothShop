using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.DataLayer.Entities.Product;

public class ProductComment
{
    [Key]
    public int CommentId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    [MaxLength(700)]
    public string Comment { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsDelete { get; set; }
    public bool IsAdminRead { get; set; }

    #region Relations

    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    [ForeignKey("UserId")]
    public User.User User { get; set; }
    #endregion
}