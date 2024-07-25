using ClothShop.Core.DTOs.Product;
using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ClothShop.Web.Pages.Admin.Product;
[PermissionChecker(9)]
public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public List<ShowProductForAdmin> Products { get; set; }
    public void OnGet()
    {
        Products = _productService.GetProductsForAdmin();
    }
}