using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public interface IDayAccurateDate : IMonthAccurateDate
  {
    int Day { get; }
  }
}
