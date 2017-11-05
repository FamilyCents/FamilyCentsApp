using FamilyCents.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class CustomerBalance : ICustomerScoped
  {
    public decimal Balance { get; internal set; }
    public int CustomerId { get; internal set; }
  }
}
