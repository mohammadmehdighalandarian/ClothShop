using ClothShop.Core.DTOs.User;
using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Users;

[PermissionChecker(3)]
public class EditUserModel : PageModel
{
    private IUserService _userService;
    private IPermissionService _permissionService;

    public EditUserModel(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }

        

        
    [BindProperty]
    public EditUserViewModel EditUserViewModel { get; set; }
    public void OnGet(int id)
    {
        EditUserViewModel = _userService.GetUserForShowInEditMode(id);
        ViewData["Roles"] = _permissionService.GetRoles();
    }

    public IActionResult OnPost(List<int> SelectedRoles)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _userService.EditUserFromAdmin(EditUserViewModel);

        //Edit Roles
        _permissionService.EditRolesUser(EditUserViewModel.UserId,SelectedRoles);
        return RedirectToPage("Index");
    }
}