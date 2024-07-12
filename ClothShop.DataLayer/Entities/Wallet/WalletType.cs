using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothShop.DataLayer.Entities.Wallet;

public class WalletType
{
    public WalletType()
    {

    }

    [Key]
    public int TypeId { get; set; }

    [Required]
    [MaxLength(150)]
    public string TypeTitle { get; set; }

    #region Relations

    public List<Wallet> Wallets { get; set; }

    #endregion
}