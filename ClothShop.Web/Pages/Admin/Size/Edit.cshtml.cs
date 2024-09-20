using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product.ProductDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Size
{
    [PermissionChecker(7)]
    public class EditModel : PageModel
    {
        private IProductService _productService;

        public EditModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public DataLayer.Entities.Product.ProductDetails.Size Size { get; set; }
        public void OnGet(int id)
        {
            Size = _productService.GetSize(id);
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //    return Page();

            _productService.Updatesize(Size);

            return RedirectToPage("Index");
        }
    }
}