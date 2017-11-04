using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public interface IMonthAccurateDate
  {
    Month Month { get; }
    int Year { get; }
  }
}
