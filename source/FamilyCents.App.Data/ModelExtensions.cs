using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FamilyCents.App.Data
{
  public static class ModelExtensions
  {
    public static DateTimeOffset ToDateTimeOffset(this IMonthAccurateDate date)
    {
      return new DateTimeOffset(date.Year, (int)date.Month, 1, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset ToDateTimeOffset(this IDayAccurateDate date)
    {
      return new DateTimeOffset(date.Year, (int)date.Month, date.Day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset ToNextMonth(this DateTimeOffset date)
    {
      return date.Date.AddMonths(1);
    }
  }
}
