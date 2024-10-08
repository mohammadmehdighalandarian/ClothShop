﻿using ClothShop.Core.DTOs.User;
using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Users;

[PermissionChecker(2)]
public class CreateUserModel : PageModel
{
    private IUserService _userService;
    private IPermissionService _permissionService;

    public CreateUserModel(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }

        
    [BindProperty]
    public CreateUserViewModel CreateUserViewModel { get; set; }

    public void OnGet()
    {
        ViewData["Roles"] = _permissionService.GetRoles();
    }

    public IActionResult OnPost(List<int> SelectedRoles)
    {
        if (!ModelState.IsValid)
            return Page();

        int userId = _userService.AddUserFromAdmin(CreateUserViewModel);

        //Add Roles
        _permissionService.AddRolesToUser(SelectedRoles,userId);


        return Redirect("/Admin/Users");

    }
}