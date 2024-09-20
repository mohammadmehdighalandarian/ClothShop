using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClothShop.Web.ViewComponent;

public class ProductGroupComponent : Microsoft.AspNetCore.Mvc.ViewComponent
{
    private readonly IProductService _productService;

    public ProductGroupComponent(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult((IViewComponentResult)View("ProductGroup", _productService.GetAllGroup()));
    }
}