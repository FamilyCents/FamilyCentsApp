using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public interface ICustomerScoped
  {
    int CustomerId { get; }
  }
}
