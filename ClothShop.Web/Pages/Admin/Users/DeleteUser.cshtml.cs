using ClothShop.Core.DTOs.User;
using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Users;

[PermissionChecker(5)]
public class DeleteUserModel : PageModel
{
    private IUserService _userService;

    public DeleteUserModel(IUserService userService)
    {
        _userService = userService;
    }

    public InformationUserViewModel InformationUserViewModel { get; set; }
    public void OnGet(int id)
    {
        ViewData["UserId"] = id;
        InformationUserViewModel = _userService.GetUserInformation(id);
    }

    public IActionResult OnPost(int UserId)
    {
        _userService.DeleteUser(UserId);
        return RedirectToPage("Index");
    }
}