using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on your payment history
  /// 35%
  /// </summary>
  public class PaymentScore : IScoreComponent
  {

    private struct AggregatePayment
    {
      public int CurrentCombo;
      public double CurrentPenalty;
      public DateTimeOffset DueDate;
      
    }

    private const double _maxScore = 1000;

    private const int _monthsBack = 12 * 5;

    private const double _percentageHitPerBadPayment = 0.05;

    public double Score { get => CalculateScore(this.Payments, DateTimeOffset.UtcNow); }

    public List<Payment> Payments { get; private set; }

    public PaymentScore(List<Payment> payments)
    {
      this.Payments = payments;
    }

    private double CalculateScore(List<Payment> payments, DateTimeOffset now)
    {
      var result = _maxScore;

      var failurePercentage = this.Payments
        .Where(rec => rec.DateDue >= now.AddMonths(-(_monthsBack)))
        .GroupBy(rec => rec.DateDue.Date)
        .Where(group => !group.Any(rec => rec.MinimumRemaining == 0))
        .Select(group => new AggregatePayment() { CurrentCombo = 0, CurrentPenalty = 0, DueDate = group.First().DateDue })
        .OrderBy(rec => rec.DueDate)
        .Aggregate((previous, current) =>
        {
          current.CurrentCombo++;
          if (previous.DueDate == current.DueDate)
          {
            current.CurrentCombo = previous.CurrentCombo + 1;
          }
          else
          {
            current.CurrentCombo = 1;
          }

          current.CurrentPenalty = previous.CurrentPenalty + (_percentageHitPerBadPayment * current.CurrentCombo);


          return current;
        }).CurrentPenalty;

      if (failurePercentage >= 1)
      {
        result = 0;
      } else
      {
        result = _maxScore - (_maxScore * failurePercentage);
      }
                

      return result;
    }
    
  }
}
