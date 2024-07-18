using ClothShop.Core.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClothShop.Web.Pages.Admin;

[PermissionChecker(1)]
public class IndexModel : PageModel
{
    public void OnGet()
    {

    }
}