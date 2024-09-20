using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Size;

[PermissionChecker(6)]
public class AddModel : PageModel
{
    private IProductService _productService;

    public AddModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public DataLayer.Entities.Product.ProductDetails.Size size{ get; set; }

    public void OnGet()
    {
        ViewData["Size"] = _productService.GetAllSizes();
    }

    public IActionResult OnPost()
    {
        //if (!ModelState.IsValid)
        //    return Page();

        size.IsActive = true;
        _productService.Addsize(size);

        return RedirectToPage("Index");
    }
}