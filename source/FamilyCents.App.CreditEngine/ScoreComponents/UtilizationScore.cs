using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{

  /// <summary>
  /// Score based on the percentage of credit to debt utilized
  /// </summary>
  public class UtilizationScore : IScoreComponent
  {
    public int Score { get => 0; }
  }
}
