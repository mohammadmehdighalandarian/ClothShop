namespace ClothShop.Core.DTOs.User;

public class InformationUserViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Wallet { get; set; }
    public string? Address { get; set; }
}