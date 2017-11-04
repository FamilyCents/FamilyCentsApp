using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  /// <summary>
  /// Score based on the length of time credit line has been opened
  /// 15%
  /// </summary>
  public class LifespanScore : IScoreComponent
  {
    private const double _maxScore = 1000;

    private const int _minYearsForMax = 7;

    public double Score { get => CalculateScore(DateTimeOffset.UtcNow); }

    public DateTimeOffset AccountOpened { get; private set; }

    public LifespanScore(DateTimeOffset accountOpenDate)
    {
      this.AccountOpened = accountOpenDate;
    }

    /// <summary>
    /// 7 year minimum needed for a perfect score. 
    /// </summary>
    /// <param name="now"></param>
    /// <returns></returns>
    public double CalculateScore(DateTimeOffset now)
    {
      var result = 0d;
      if (now.Date > this.AccountOpened.Date)
      {
        if ((now.Year - this.AccountOpened.Year) >=7)
        {
          result = _maxScore;
        }
        else
        {
          result = (int)(_maxScore * 
                          Math.Log(1 + GetNumberOfYears(now, this.AccountOpened), 
                          _minYearsForMax + 1));
        }
      }
      //else if (now.Date == this.AccountOpened.Date)
      else
      {

      }
      return result;
    }

    private static double GetNumberOfYears(DateTimeOffset now, DateTimeOffset openDate)
    {
      var result = 0d;

      result = ((now - openDate).Days / 365.25);

      if (result > _minYearsForMax) result = _minYearsForMax;
      return result;
    }
  }


}
