using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Product
{
    public class DeActiveProductModel : PageModel
    {
        private readonly IProductService _productService;

        public DeActiveProductModel(IProductService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public DataLayer.Entities.Product.Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _productService.GetProductById(id);
        }

        public IActionResult OnPost()
        {
            _productService.DeActiveProduct(Product);
            return RedirectToPage("index");
        }
    }
}


