using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on your payment history
  /// </summary>
  public class PaymentScore : IScoreComponent
  {
    public int Score { get => 0; }
  }
}
