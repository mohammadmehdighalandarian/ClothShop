using ClothShop.Core.DTOs.User;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Users
{
    public class ActiveUserModel : PageModel
    {
        private readonly IUserService _userService;
        public InformationUserViewModel InformationUserViewModel { get; set; }
        public ActiveUserModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = _userService.GetUserInformation(id);
        }

        public IActionResult OnPost(int UserId)
        {
            _userService.ActiveUser(UserId);
            return RedirectToPage("ListDeleteUsers");
        }
        
    }
}
