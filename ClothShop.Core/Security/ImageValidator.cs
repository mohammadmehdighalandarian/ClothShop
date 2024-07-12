﻿using Microsoft.AspNetCore.Http;

namespace ClothShop.Core.Security;

public static class ImageValidator
{
    public static bool IsImage(this IFormFile file)
    {
        try
        {
            var img = System.Drawing.Image.FromStream(file.OpenReadStream());
            return true;
        }
        catch
        {
            return false;
        }
    }
}