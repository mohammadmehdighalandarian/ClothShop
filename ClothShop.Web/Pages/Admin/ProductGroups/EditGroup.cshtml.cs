using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.ProductGroups;

public class EditGroupModel : PageModel
{
    private IProductService _productService;

    public EditGroupModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ProductGroup ProductGroup { get; set; }

    public void OnGet(int id)
    {
        ProductGroup = _productService.GetById(id);
    }

    public IActionResult OnPost()
    {
        //if (!ModelState.IsValid)
        //    return Page();

        _productService.UpdateGroup(ProductGroup);

        return RedirectToPage("Index");
    }
}