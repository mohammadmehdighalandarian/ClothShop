using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Material;

[PermissionChecker(7)]
public class EditModel : PageModel
{
    private IProductService _productService;

    public EditModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public DataLayer.Entities.Product.ProductDetails.Material Material { get; set; }
    public void OnGet(int id)
    {
        Material = _productService.GetMaterial(id);
    }

    public IActionResult OnPost()
    {
        //if (!ModelState.IsValid)
        //    return Page();

        _productService.UpdateMatrial(Material);

        return RedirectToPage("Index");
    }
}