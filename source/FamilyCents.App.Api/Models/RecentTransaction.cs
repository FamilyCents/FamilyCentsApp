using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Models
{
  public class RecentTransaction
  {
    public decimal Value { get; set; }
    public string Merchant { get; set; }
    public string When { get; internal set; }
  }
}
