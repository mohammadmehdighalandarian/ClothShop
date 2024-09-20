using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClothShop.Web.ViewComponent
{
    public class LatesProductViewComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private IProductService _productService;

        public LatesProductViewComponent(IProductService productService)
        {
            _productService= productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatesProduct",_productService.GetProduct().Item1));
        }
    }
}
