using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Material;

[PermissionChecker(5)]
public class IndexModel : PageModel
{
    private IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public List<DataLayer.Entities.Product.ProductDetails.Material> ItemList { get; set; }


    public void OnGet()
    {
        ItemList = _productService.GetAllMatrial();
    }

    public IActionResult OnPostActive(int id)
    {
        var material = _productService.GetMaterial(id);
        material.IsActive = true;
        _productService.UpdateMatrial(material);

        return RedirectToPage("Index");
    }

    public IActionResult OnPostDeActive(int id)
    {
        var material = _productService.GetMaterial(id);
        material.IsActive = false;
        _productService.UpdateMatrial(material);

        return RedirectToPage("Index");
    }


}