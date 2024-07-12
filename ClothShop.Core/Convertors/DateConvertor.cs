﻿using System.Globalization;

namespace ClothShop.Core.Convertors;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime value)
    {
        PersianCalendar pc=new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00");
    }
}