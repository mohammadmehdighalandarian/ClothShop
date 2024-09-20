using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Material;

[PermissionChecker(6)]
public class AddModel : PageModel
{
    private IProductService _productService;

    public AddModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public DataLayer.Entities.Product.ProductDetails.Material Material{ get; set; }

    public void OnGet()
    {
        ViewData["Material"] = _productService.GetAllMatrial();
    }

    public IActionResult OnPost()
    {
        //if (!ModelState.IsValid)
        //    return Page();

        Material.IsActive = true;
        _productService.AddMatrial(Material);

        return RedirectToPage("Index");
    }
}