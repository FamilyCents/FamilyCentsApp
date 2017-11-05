using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCents.App.Api.Models
{
  public class FamilyMember
  {
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public decimal VirtualBalance { get; set; }
    public int VirtualCreditScore { get; set; }
    public decimal VirtualCreditLimit { get; set; }
    public bool IsPrimary { get; set; }
    public List<RecentTransaction> RecentTransactions { get; set; }
  }
}
