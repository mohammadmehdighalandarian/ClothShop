using ClothShop.Core.Service;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.ProductGroups;

public class CreateGroupModel : PageModel
{
    private IProductService _productService;

    public CreateGroupModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ProductGroup ProductGroup { get; set; }

    public void OnGet(int? id)
    {
        ProductGroup = new ProductGroup()
        {
            ParentId = id
        };
    }

    public IActionResult OnPost()
    {
        //if (!ModelState.IsValid)
        //    return Page();

        _productService.AddGroup(ProductGroup);

        return RedirectToPage("Index");
    }
}