using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.ProductGroups
{
    public class IndexModel : PageModel
    {
        private IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<ProductGroup> ProductGroups { get; set; }
        public void OnGet()
        {
            ProductGroups = _productService.GetAllGroup();
        }
    }
}