using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine
{
  public class CreditLimitChange
  {

    public DateTimeOffset AdjustmentDate { get; private set; }

    public double CurrentBalance { get; private set; }

    public double CurrentLimit { get; private set; }

    public double NewLimit { get; private set; }


    public CreditLimitChange(double currentBalance, double currentLimit, double newLimit, DateTimeOffset adjustmentDate)
    {
      this.CurrentBalance = currentBalance;
      this.CurrentLimit = currentLimit;
      this.NewLimit = newLimit;
      this.AdjustmentDate = adjustmentDate;
    }
    
  }
}
