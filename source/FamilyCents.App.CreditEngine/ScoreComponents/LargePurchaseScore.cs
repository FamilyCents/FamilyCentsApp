using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on specific purchases and their 
  /// 10%
  /// </summary>
  public class LargePurchaseScore : IScoreComponent
  {
    public double Score { get => 0; }
  }
}
