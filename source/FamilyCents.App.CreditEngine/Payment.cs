using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine
{
  public class Payment
  {

    public DateTimeOffset DateReceived { get; private set; }
    public DateTimeOffset DateDue { get; private set; }
    public double MinimumDue { get; private set; }
    public double MinimumRemaining { get; private set; }
    public double Amount { get; private set; }


    public Payment(DateTimeOffset dateReceived, DateTimeOffset dateDue, double minimumDue, double minimumRemaining, double amount)
    {
      this.DateDue = dateDue;
      this.DateReceived = dateReceived;
      this.MinimumDue = minimumDue;
      this.MinimumRemaining = minimumRemaining;
      this.Amount = amount;
    }
  }
}
