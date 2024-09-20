namespace ClothShop.Core.DTOs.Product;

public class ShowProductListItemViewModel
{
    public int ProductId { get; set; }
    public string Title { get; set; }
    public string ImageName { get; set; }
    public int Price { get; set; }
    public int GroupId { get; set; }
    public int subGroupId { get; set; } = -1;
}