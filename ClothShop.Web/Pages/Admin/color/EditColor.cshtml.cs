using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product.ProductDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.color
{
    [PermissionChecker(7)]
    public class EditColorModel : PageModel
    {
        private IProductService _productService;

        public EditColorModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Color color { get; set; }
        public void OnGet(int id)
        {
            color = _productService.GetColor(id);
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //    return Page();

            _productService.UpdateColor(color);

            return RedirectToPage("Index");
        }
    }
}