using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on the length of time credit line has been opened
  /// </summary>
  public class LifespanScore : IScoreComponent
  {
    public int Score { get => 0; }
  }
}
