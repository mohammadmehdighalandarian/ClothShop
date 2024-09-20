using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product.ProductDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.color;

[PermissionChecker(6)]
public class AddColorModel : PageModel
{
    private IProductService _productService;

    public AddColorModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public Color color { get; set; }

    public void OnGet()
    {
        ViewData["Color"] = _productService.GetAllColor();
    }

    public IActionResult OnPost()
    {
        //if (!ModelState.IsValid)
        //    return Page();

        color.isActive = true;
        _productService.AddColor(color);

        return RedirectToPage("Index");
    }
}