using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.DataLayer.Entities.User;

public class UserDiscountCode
{
    [Key]
    public int UserDiscount_Id { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    [ForeignKey("DiscountId")]
    public int DiscountId { get; set; }

    
}