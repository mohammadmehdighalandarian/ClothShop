﻿using ClothShop.Core.DTOs.User;
using ClothShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Users;

//[PermissionChecker(2)]
public class IndexModel : PageModel
{
    private IUserService _userService;

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    public UserForAdminViewModel UserForAdminViewModel { get; set; }

    public void OnGet(int pageId=1,string filterUserName="",string filterEmail="")
    {
        UserForAdminViewModel = _userService.GetUsers(pageId,filterEmail,filterUserName);
    }

       
}