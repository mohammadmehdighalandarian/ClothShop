using ClothShop.Core.DTOs.User;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Users;

public class ListDeleteUsersModel : PageModel
{
    private IUserService _userService;

    public ListDeleteUsersModel(IUserService userService)
    {
        _userService = userService;
    }

    public UserForAdminViewModel UserForAdminViewModel { get; set; }

    public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
    {
        UserForAdminViewModel = _userService.GetDeleteUsers(pageId, filterEmail, filterUserName);
    }

}