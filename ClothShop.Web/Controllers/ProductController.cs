using ClothShop.Core.DTOs.Product;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private IOrderService _orderService;
    private IUserService _userService;


    private readonly int _ProdoctId;

    public ProductController(IProductService productService, IOrderService orderService, IUserService userService)
    {
        _productService = productService;
        _orderService = orderService;
        _userService = userService;
    }

    public IActionResult Index(int pageId = 1, string filter = ""
        , string getType = "all", string orderByType = "date",
        int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
    {
         
        ViewBag.Groups = _productService.GetAllGroup();
        ViewBag.pageId = pageId;
        var products = _productService.GetProduct
        (pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, 9);

        ViewBag.selectedGroups = _productService.GetGroupsOfProduct(products.Item1);

        return View(products);
    }


    [Route("ShowProduct/{id}")]
    public IActionResult ShowProduct(int id, int episode = 0)
    {
        var product = _productService.GetProductForShow(id);

        var Sizes = _productService.GetSizesOfProduct(id);
        ViewData["Sizes"] = Sizes;

        var Images = _productService.GetAllImagesOfProduct(id);
        // 2. Get image paths for the product
        string[] imagePaths = product.Images.Select(i => $"/Product/image/{i.ImageName}").ToArray();

        // 3. Pass the image paths to the view
        ViewData["ImagePaths"] = imagePaths;


        List<SelectListItem> Colorlist = new List<SelectListItem>()
        {
            new SelectListItem(){Text = "مشكي",Value = "1"},
            new SelectListItem(){Text = "کرم",Value = "2"},
            new SelectListItem(){Text = "سورمه ای",Value = "3"},
            new SelectListItem(){Text = "سبز",Value = "4"}
        };
        ViewData["Color"] = new SelectList(Colorlist, "Value", "Text");

        var material = _productService.GetMaterailOfProduct(id);
        ViewData["Material"]= material;


        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [Authorize]
    public ActionResult BuyProduct(BuyProductViewModel buyProduct)
    {
        int orderId = _orderService.AddOrder(User.Identity.Name, buyProduct.ProductId);
        return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId);
    }


    [HttpPost]
    public IActionResult CreateComment( ProductComment comment)
    {
        comment.IsDelete = false;
        comment.CreateDate = DateTime.Now;
        comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
        comment.IsAdminRead = false;
        _productService.AddComment(comment);

        return View("ShowComment", _productService.GetProductComment(comment.ProductId));
    }

    public IActionResult ShowComment(int id, int pageId = 1)
    {
        return View(_productService.GetProductComment(id, pageId));
    }

    //public IActionResult CourseVote(int Id)
    //{
    //    if (!_courseService.IsFree(Id))
    //    {
    //        if (!_orderService.IsUserInCourse(User.Identity.Name, Id))
    //        {
    //            ViewBag.NotAccess = true;
    //        }
    //    }
    //    return PartialView(_courseService.GetCourseVotes(Id));
    //}

    //[Authorize]
    //public IActionResult AddVote(int id,bool vote)
    //{
    //    _courseService.AddsVote(_userService.GetUserIdByUserName(User.Identity.Name),id,vote);

    //    return PartialView("CourseVote",_courseService.GetCourseVotes(id));
    //}


}