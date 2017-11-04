using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on how quickly credit limit increases are consumed
  /// 10%
  /// </summary>
  public class ConsumptionScore : IScoreComponent
  {
    public double Score { get => 0; }
  }
}
