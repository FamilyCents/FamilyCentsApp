using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on specific purchases and their 
  /// 10%
  /// </summary>
  public class LargePurchaseScore : IScoreComponent
  {
    private const double _maxScore = 1000;

    private const int _minDaysForMax = 120;

    private const int _maxAcceptableLargePurchases = _minDaysForMax / 30;
    private const double _acceptablePercentage = 0.05;

    public double Score { get => CalculateScore(this.RecentTransactions, this.CreditLimit, DateTimeOffset.UtcNow); }

    public List<Transaction> RecentTransactions { get; private set; }

    public double CreditLimit { get; private set; }

    public LargePurchaseScore(List<Transaction> recentTransactions, double creditLimit)
    {
      this.RecentTransactions = recentTransactions;
      this.CreditLimit = creditLimit;
    }

    private double CalculateScore(List<Transaction> recentTransactions, double creditLimit, DateTimeOffset now)
    {
      var result = _maxScore;
      var acceptablePercentageAmount = creditLimit * _acceptablePercentage;

      var timeframedTransactions = recentTransactions.Where(rec => (now.AddDays(-(_minDaysForMax)) < rec.ActivityDate) && (rec.Amount >= acceptablePercentageAmount)).ToList();
      if (timeframedTransactions.Any())
      {
        if (timeframedTransactions.Count() >= _maxAcceptableLargePurchases)
        {
          result = 0;
        }
        else
        {
          var percentPerTransaction = _maxScore/ _maxAcceptableLargePurchases;

          result = _maxScore - (timeframedTransactions.Count() * percentPerTransaction);
        }
      }

      return result;
    }
  }
}
