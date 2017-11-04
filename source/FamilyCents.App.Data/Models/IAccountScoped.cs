using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public interface IAccountScoped
  {
    int AccountId { get; }
  }
}
