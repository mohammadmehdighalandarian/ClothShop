using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ClothShop.Core.DTOs.User;

public class EditProfileViewModel
{
    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string UserName { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
    public string Email { get; set; }

    public IFormFile UserAvatar { get; set; }

    public string AvatarName { get; set; }

    [Display(Name = "استان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Province { get; set; }

    [Display(Name = "شهر")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string City { get; set; }

    [Display(Name = "محله")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Neighborhood { get; set; }

    [Display(Name = "واحد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ApartmentNo { get; set; }

    [Display(Name = "پلاك")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Plate { get; set; }

    [Display(Name = "كد پستي")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string PostCode { get; set; }

    [Display(Name = "نام گيرنده")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string RecieverFName { get; set; }

    [Display(Name = "نام خانوادگي گيرنده")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string RecieverLName { get; set; }

    [Display(Name = "شماره تماس گيرنده")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string RecieverPhoneNo { get; set; }
}