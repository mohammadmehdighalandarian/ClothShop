﻿using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin.Roles
{
    [PermissionChecker(8)]
    public class DeleteRoleModel : PageModel
    {
        private IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            _permissionService.DeleteRole(Role);

            return RedirectToPage("Index");
        }
    }
}