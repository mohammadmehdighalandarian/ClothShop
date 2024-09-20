using ClothShop.Core.DTOs.Product;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductModel=ClothShop.DataLayer.Entities.Product.Product;

namespace ClothShop.Web.Pages.Admin.Product;

public class CreateProductModel : PageModel
{
    private readonly IProductService _productService;

    public CreateProductModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty] 
    public ProductModel Product { get; set; }

    
    [BindProperty]
    public List<SizeWithCountForAdmin> SizesWithCount { get; set; }

    public void OnGet()
    {
        var groups = _productService.GetGroupForManageProduct();
        ViewData["Groups"] = new SelectList(groups, "Value", "Text");

        var subGrous = _productService.GetSubGroupForManageProduct(int.Parse(groups.First().Value));
        ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

        var materials = _productService.GetMaterials();
        ViewData["Materials"] = materials;

        var sizes = _productService.GetAllSizes();
        ViewData["Sizes"] = sizes;

        var statues = _productService.GetUseTypes();
        ViewData["UseTypes"] = statues;
    }

    public IActionResult OnPost(IFormFile[] imgProductUp, List<int> materials, List<int> usetypes)
    {
        // TODO: Check Model
        // if (!ModelState.IsValid)
        // {
        //     return Page();
        // }

        int ProductId = _productService.AddProduct(Product, imgProductUp.FirstOrDefault()); // Assuming first image is the main one
        _productService.AddMaterialToProductMaterial(ProductId, materials);
        _productService.AddSizeToProductSize(ProductId, SizesWithCount);
        _productService.AddusetypeToProductusetype(ProductId, usetypes);
        

        foreach (var image in imgProductUp)
        {
            _productService.AddProductImage(ProductId, image);
        }

        return RedirectToPage("Index");
    }
}