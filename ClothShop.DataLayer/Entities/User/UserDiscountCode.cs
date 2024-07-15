using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothShop.DataLayer.Entities.Order;

namespace ClothShop.DataLayer.Entities.User;

public class UserDiscountCode
{
    [Key]
    public int UserDiscount_Id { get; set; }
    public int UserId { get; set; }
    public int DiscountId { get; set; }


    #region Relations

    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("DiscountId")]
    public Discount Discount { get; set; }

    #endregion

}