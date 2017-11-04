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
    private const double _maxScore = 1000;

    private const int _maxPercentForMax = 30;

    public double Score { get => CalculateScore(this.CurrentBalance); }

    public double CreditLimit { get; private set; }
    public double CurrentBalance { get; private set; }

    public UtilizationScore(double creditLimit, double currentBalance)
    {
      this.CreditLimit = creditLimit;
      this.CurrentBalance = currentBalance;
    }

    private double CalculateScore(double currentBalance)
    {
      var result = 0d;

      var utilization = Math.Round(currentBalance / this.CreditLimit, 2, MidpointRounding.AwayFromZero); //Credit utilization percentage.

      if (utilization <=0.30)
      {
        result = _maxScore;
      }
      else if (utilization > 0.30 && utilization < 1) //55 point range. 
      {
        var utilPercent = (Math.Round((utilization - 0.30) / (0.70), 2, MidpointRounding.AwayFromZero));

        var logPercent = Math.Pow(0.8, (utilPercent * 100)/20);

        result = (logPercent) * _maxScore;
      }
      else // > 0.85
      {
        result = 0;
      }
      
      return Math.Round(result, 2, MidpointRounding.AwayFromZero);
    }
  }
}
