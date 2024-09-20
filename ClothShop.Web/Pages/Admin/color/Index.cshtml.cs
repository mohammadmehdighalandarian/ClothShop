using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product.ProductDetails;
using ClothShop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.color;

[PermissionChecker(5)]
public class IndexModel : PageModel
{
    private IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public List<Color> colorList { get; set; }


    public void OnGet()
    {
        colorList = _productService.GetAllColor();
    }

    public IActionResult OnPostActive(int id)
    {
        var color = _productService.GetColor(id);
        color.isActive = true;
        _productService.UpdateColor(color);

        return RedirectToPage("Index");
    }

    public IActionResult OnPostDeActive(int id)
    {
        var color = _productService.GetColor(id);
        color.isActive = false;
        _productService.UpdateColor(color);

        return RedirectToPage("Index");
    }


}