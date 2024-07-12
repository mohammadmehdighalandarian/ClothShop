using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.DataLayer.Entities.User;

public class UserRole
{
    public UserRole()
    {

    }

    [Key]
    public int UserRole_Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }


    #region Relations

    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    #endregion
}