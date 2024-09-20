using ClothShop.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothShop.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductApiController : Controller
{
    private readonly ShopContext _shopContext;

    public ProductApiController(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    [Produces("application/json")]
    [HttpGet("search")]
    public async Task<IActionResult> Search()
    {
        try
        {
            var cookies = Request.Cookies;
            string term = HttpContext.Request.Query["term"].ToString();
            var ProductTitle = _shopContext.Products
                .Where(c => c.ProductTitle.Contains(term))
                .Select(c => c.ProductTitle)
                .ToList();
            return Ok(ProductTitle);
        }
        catch
        {
            return BadRequest();
        }
    }
}