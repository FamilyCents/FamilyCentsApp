using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine
{
  public class Transaction
  {
    public DateTimeOffset ActivityDate { get; private set; }

    public double Amount { get; private set; }

  }
}
