using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on your payment history
  /// 35%
  /// </summary>
  public class PaymentScore : IScoreComponent
  {
    public double Score { get => 0; }
  }
}
