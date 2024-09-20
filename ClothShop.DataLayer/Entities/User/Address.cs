using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.DataLayer.Entities.User;

public class Address
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string ApartmentNo { get; set; }
    public string Plate { get; set; }
    public string PostCode { get; set; }
    public string RecieverFName { get; set; }
    public string RecieverLName { get; set; }
    public string RecieverPhoneNo { get; set; }
    public bool IsActive { get; set; }

    #region Relation

    [ForeignKey("UserId")]
    public User User { get; set; }  

    #endregion
}