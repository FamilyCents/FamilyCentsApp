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

    private const double _maxScore = 1000;

    private const int _minDaysForMax = 30;

    public double Score { get => CalculateScore(this.PreviousLimitChange, this.CurrentBalance, DateTimeOffset.UtcNow); }

    public CreditLimitChange PreviousLimitChange { get; private set; }
    public double CurrentBalance { get; private set; }

    public ConsumptionScore(CreditLimitChange previousChange, double currentBalance)
    {
      this.PreviousLimitChange = previousChange;
      this.CurrentBalance = currentBalance;
    }

    private double CalculateScore(CreditLimitChange previousChange, double currentBalance, DateTimeOffset now)
    {
      var result = _maxScore;
      if (now.Subtract(previousChange.AdjustmentDate).Days <= _minDaysForMax)
      {
        if (((currentBalance - previousChange.CurrentBalance)/previousChange.NewLimit) >= 0.1) {
          result = 500;
        }
      }

      return result;
    }
  }
}
