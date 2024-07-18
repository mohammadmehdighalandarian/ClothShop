using ClothShop.DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.DataLayer.Entities.Permission;

public class RolePermission
{
    [Key]
    public int RolePemission_Id { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    #region Ralations
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
    [ForeignKey("PermissionId")]
    public Permission Permission { get; set; }

    #endregion

}