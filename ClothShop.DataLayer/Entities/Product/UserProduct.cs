using System.ComponentModel.DataAnnotations;

namespace ClothShop.DataLayer.Entities.Product;

public class UserProduct
{
    [Key]
    public int UserProduct_Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }


    #region Relations

    public User.User Users { get; set; }
    public Product Products { get; set; }

    #endregion
}