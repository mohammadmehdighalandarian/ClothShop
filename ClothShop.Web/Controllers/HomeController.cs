using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothShop.Web.Controllers;

public class HomeController : Controller
{
    private IUserService _userService;
    private IProductService _productService;

    public HomeController(IUserService userService, IProductService productService)
    {
        _userService = userService;
        _productService = productService;
    }

    public IActionResult Index()
    {
        var popular = _productService.GetPopularProduct();
        ViewBag.PopularCourse = popular;
        return View(_productService.GetProduct());
    }

    [Route("/OnlinePayment/{id}")]
    public IActionResult onlinePayment(int id)
    {
        if (HttpContext.Request.Query["Status"] != "" &&
            HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
            && HttpContext.Request.Query["Authority"] != "")
        {
            string authority = HttpContext.Request.Query["Authority"];

            var wallet = _userService.GetWalletByWalletId(id);

            var payment = new ZarinpalSandbox.Payment(wallet.Amount);
            var res = payment.Verification(authority).Result;
            if (res.Status == 100)
            {
                ViewBag.code = res.RefId;
                ViewBag.IsSuccess = true;
                wallet.IsPay = true;
                _userService.UpdateWallet(wallet);
            }

        }

        return View();
    }

    public IActionResult GetSubGroups(int id)
    {
        List<SelectListItem> list = new List<SelectListItem>()
        {
            new SelectListItem(){Text = "انتخاب کنید",Value = ""}
        };
        list.AddRange(_productService.GetSubGroupForManageProduct(id));
        return Json(new SelectList(list, "Value", "Text"));
    }


    [HttpPost]
    [Route("file-upload")]
    public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
    {
        if (upload.Length <= 0) return null;

        var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



        var path = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/MyImages",
            fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            upload.CopyTo(stream);

        }



        var url = $"{"/MyImages/"}{fileName}";


        return Json(new { uploaded = true, url });
    }
}