﻿using System.ComponentModel.DataAnnotations;
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
}