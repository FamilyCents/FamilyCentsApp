using FamilyCents.App.CreditEngine.ScoreComponents;
using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using Xunit;

namespace FamilyCents.App.Tests
{
  public class CreditScoreTests
  {

    #region Lifespan
    [Fact]
    public void CalculateLifeSpanScore_8yearsMaxScore()
    {

      var lifespan = new LifespanScore(new DateTimeOffset(DateTime.UtcNow.AddYears(-8)));

      var score = lifespan.Score;

      Assert.Equal(1000, score);
    }

    [Fact]
    public void CalculateLifeSpanScore_5yearsNotMaxScore()
    {

      var lifespan = new LifespanScore(DateTimeOffset.UtcNow.AddYears(-5));

      var score = lifespan.Score;

      Assert.NotEqual(1000, score);
    }

    [Fact]
    public void CalculateLifeSpanScore_0years0Score()
    {

      var lifespan = new LifespanScore(DateTimeOffset.UtcNow);

      var score = lifespan.Score;

      Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateLifeSpanScore_NegativeYears0Score()
    {

      var lifespan = new LifespanScore(DateTimeOffset.UtcNow.AddYears(5));

      var score = lifespan.Score;

      Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateLifeSpanScore_ArbitraryYear()
    {

      var lifespan = new LifespanScore(DateTimeOffset.UtcNow.AddYears(-3).AddMonths(-6));

      var score = lifespan.Score;

      Assert.Equal(723, score);
    }

    [Fact]
    public void CalculateLifeSpanScore_1Year()
    {

      var lifespan = new LifespanScore(DateTimeOffset.UtcNow.AddYears(-1));

      var score = lifespan.Score;

      Assert.Equal(333, score);
    }


    [Fact]
    public void CalculateLifeSpanScore_1Month()
    {

      var lifespan = new LifespanScore(DateTimeOffset.UtcNow.AddMonths(-1));

      var score = lifespan.Score;

      Assert.Equal(39, score);
    }


    [Fact]
    public void CalculateLifeSpanScore_5Year()
    {

      var lifespan = new LifespanScore(DateTimeOffset.UtcNow.AddYears(-5));

      var score = lifespan.Score;

      Assert.Equal(861, score);
    }

    #endregion

    #region Utilization

    [Fact]
    public void CalculateUtilization_All()
    {

      var utilization = new UtilizationScore(10000, 10000);

      var score = utilization.Score;

      Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateUtilization_10percent()
    {

      var utilization = new UtilizationScore(10000, 1000);

      var score = utilization.Score;

      Assert.Equal(1000, score);
    }

    [Fact]
    public void CalculateUtilization_20percent()
    {

      var utilization = new UtilizationScore(10000, 2000);

      var score = utilization.Score;

      Assert.Equal(1000, score);
    }

    [Fact]
    public void CalculateUtilization_30percent()
    {

      var utilization = new UtilizationScore(10000, 3000);

      var score = utilization.Score;

      Assert.Equal(1000, score);
    }

    [Fact]
    public void CalculateUtilization_31percent()
    {

      var utilization = new UtilizationScore(10000, 3100);

      var score = utilization.Score;

      Assert.Equal(988.9, score);
    }

    [Fact]
    public void CalculateUtilization_40percent()
    {

      var utilization = new UtilizationScore(10000, 4000);

      var score = utilization.Score;

      Assert.Equal(855.39, score);
    }

    [Fact]
    public void CalculateUtilization_50percent()
    {

      var utilization = new UtilizationScore(10000, 5000);

      var score = utilization.Score;

      Assert.Equal(723.57, score);
    }

    [Fact]
    public void CalculateUtilization_60percent()
    {

      var utilization = new UtilizationScore(10000, 6000);

      var score = utilization.Score;

      Assert.Equal(618.93, score);
    }

    [Fact]
    public void CalculateUtilization_70percent()
    {

      var utilization = new UtilizationScore(10000, 7000);

      var score = utilization.Score;

      Assert.Equal(529.43, score);
    }

    [Fact]
    public void CalculateUtilization_80percent()
    {

      var utilization = new UtilizationScore(10000, 8000);

      var score = utilization.Score;

      Assert.Equal(452.87, score);
    }

    [Fact]
    public void CalculateUtilization_84percent()
    {

      var utilization = new UtilizationScore(10000, 8400);

      var score = utilization.Score;

      Assert.Equal(423.54, score);
    }

    [Fact]
    public void CalculateUtilization_90percent()
    {

      var utilization = new UtilizationScore(10000, 9000);

      var score = utilization.Score;

      Assert.Equal(383.08, score);
    }

    [Fact]
    public void CalculateUtilization_95percent()
    {

      var utilization = new UtilizationScore(10000, 9500);

      var score = utilization.Score;

      Assert.Equal(354.3, score);
    }

    [Fact]
    public void CalculateUtilization_98percent()
    {

      var utilization = new UtilizationScore(10000, 9800);

      var score = utilization.Score;

      Assert.Equal(338.83, score);
    }

    [Fact]
    public void CalculateUtilization_99percent()
    {

      var utilization = new UtilizationScore(10000, 9900);

      var score = utilization.Score;

      Assert.Equal(331.36, score);
    }



    #endregion

    #region Consumption

    [Fact]
    public void CalculateConsumption_Failed()
    {

      var consumption = new ConsumptionScore(new CreditEngine.CreditLimitChange(12000, 25000, 30000, DateTimeOffset.UtcNow.AddDays(-19)), 16000 );

      var score = consumption.Score;

      Assert.Equal(500, score);
    }

    [Fact]
    public void CalculateConsumption_Success()
    {

      var consumption = new ConsumptionScore(new CreditEngine.CreditLimitChange(12000, 25000, 30000, DateTimeOffset.UtcNow.AddDays(-19)), 14000);

      var score = consumption.Score;

      Assert.Equal(1000, score);
    }

    [Fact]
    public void CalculateConsumption_SuccessTimePassed()
    {

      var consumption = new ConsumptionScore(new CreditEngine.CreditLimitChange(12000, 25000, 30000, DateTimeOffset.UtcNow.AddDays(-31)), 16000);

      var score = consumption.Score;

      Assert.Equal(1000, score);
    }

    #endregion


    #region Payments
    #endregion

    #region LargePurchase
    #endregion

  }
}