using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClothShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IUserService _userService;

        public AddressController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("UserPanel/Addresses")]
        public IActionResult Index()
        {   
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Addresses = _userService.GetAddressByUserId(userId);
            return View(Addresses);
        }

        [Route("UserPanel/ChangeAddress")]
        [HttpPost("{addressId}")]
        public IActionResult ChangeAddress(int addressId)
        {
            int userId = _userService.ChangeAddress(addressId);
            var Addresses = _userService.GetAddressByUserId(userId);
            return View("Index", Addresses);
        }

        [Route("UserPanel/AddAddress")]
        public IActionResult AddAddress()
        {
            return View();
        }

        [Route("UserPanel/AddNewAddress")]
        public IActionResult AddNewAddress(Address address)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _userService.AddNewAddress(address, userId);
            var Addresses = _userService.GetAddressByUserId(userId);
            return View("Index", Addresses);
        }
    }
}
