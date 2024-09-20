using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Size;

[PermissionChecker(5)]
public class IndexModel : PageModel
{
    private IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public List<DataLayer.Entities.Product.ProductDetails.Size> ItemList { get; set; }


    public void OnGet()
    {
        ItemList = _productService.GetAllSizes();
    }

    public IActionResult OnPostActive(int id)
    {
        var size = _productService.GetSize(id);
        size.IsActive = true;
        _productService.Updatesize(size);

        return RedirectToPage("Index");
    }

    public IActionResult OnPostDeActive(int id)
    {
        var size = _productService.GetSize(id);
        size.IsActive = false;
        _productService.Updatesize(size);

        return RedirectToPage("Index");
    }


}