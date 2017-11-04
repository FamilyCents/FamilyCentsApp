using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{

  /// <summary>
  /// Score based on the percentage of credit to debt utilized
  /// 30%
  /// </summary>
  public class UtilizationScore : IScoreComponent
  {
    public double Score { get => 0; }
  }
}
