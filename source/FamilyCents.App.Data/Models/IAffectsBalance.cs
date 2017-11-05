using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  interface IAffectsBalance
  {
    decimal Amount { get; }
  }
}
