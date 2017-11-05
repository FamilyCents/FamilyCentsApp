using FamilyCents.App.CreditEngine.ScoreComponents;
using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    [Fact]
    public void CalculatePayment()
    {

      var now = DateTimeOffset.UtcNow;

      var paymentList = new List<CreditEngine.Payment>
      {
        new CreditEngine.Payment(now.AddMonths(-1), now.AddMonths(-1).AddDays(-1), 75, 0, 75)
      };

      var payment = new PaymentScore(paymentList);

      var score = payment.Score;

      Assert.Equal(1000, score);
    }

    [Fact]
    public void CalculatePayment_missedPayments()
    {

      var now = DateTimeOffset.UtcNow;

      var paymentList = new List<CreditEngine.Payment>
      {
        new CreditEngine.Payment(now.AddMonths(-1).AddDays(-5), now.AddMonths(-1).AddDays(-5), 75, 75, 0)
      };

      var payment = new PaymentScore(paymentList);

      var score = payment.Score;

      Assert.Equal(950, score);
    }

    [Fact]
    public void CalculatePayment_missedPayments_multimonth()
    {

      var now = DateTimeOffset.UtcNow;

      var paymentList = new List<CreditEngine.Payment>
      {
        new CreditEngine.Payment(now.AddMonths(-4).AddDays(-5), now.AddMonths(-4).AddDays(-5), 75, 75, 0),
        new CreditEngine.Payment(now.AddMonths(-3).AddDays(-5), now.AddMonths(-3).AddDays(-5), 75, 75, 0),
        new CreditEngine.Payment(now.AddMonths(-2).AddDays(-5), now.AddMonths(-2).AddDays(-5), 75, 75, 0),
        new CreditEngine.Payment(now.AddMonths(-1).AddDays(-5), now.AddMonths(-1).AddDays(-5), 75, 75, 0)
      };

      var payment = new PaymentScore(paymentList);

      var score = payment.Score;

      Assert.Equal(500, score);
    }

    [Fact]
    public void CalculatePayment_missedPayments_multimonth_skip()
    {

      var now = DateTimeOffset.UtcNow;

      var paymentList = new List<CreditEngine.Payment>
      {
        new CreditEngine.Payment(now.AddMonths(-4).AddDays(-5), now.AddMonths(-4).AddDays(-5), 75, 75, 0),
        new CreditEngine.Payment(now.AddMonths(-3).AddDays(-5), now.AddMonths(-3).AddDays(-5), 75, 75, 0),
        new CreditEngine.Payment(now.AddMonths(-2).AddDays(-5), now.AddMonths(-2).AddDays(-5), 75, 0, 75),
        new CreditEngine.Payment(now.AddMonths(-1).AddDays(-5), now.AddMonths(-1).AddDays(-5), 75, 75, 0)
      };

      var payment = new PaymentScore(paymentList);

      var score = payment.Score;

      Assert.Equal(800, score);
    }

    #endregion

    #region LargePurchase

    [Fact]
    public void CalculateLargePurchase()
    {

      var listOfPurchases = new List<CreditEngine.Transaction>()
      {
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays( -5), 90),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-10), 85),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-15), 80),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-20), 75),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-25), 70),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-30), 65),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-35), 60),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-40), 55),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-45), 50),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-50), 45),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-55), 40),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-60), 35),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-65), 30),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-70), 25),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-75), 20),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-80), 15),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-85), 10),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-90),  5),
      };


      var largePurchase = new LargePurchaseScore(listOfPurchases, 1750); //87.5

      var score = largePurchase.Score;

      Assert.Equal(750, score);
    }

    [Fact]
    public void CalculateLargePurchase_outsideRange()
    {


      var listOfPurchases = new List<CreditEngine.Transaction>()
      {
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays( -5), 90),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-10), 85),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-15), 80),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-20), 75),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-25), 70),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-30), 65),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-35), 60),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-40), 55),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-45), 50),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-50), 45),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-55), 40),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-60), 35),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-65), 30),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-70), 25),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-75), 20),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-80), 15),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-85), 10),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-90),  5),

        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-121),  500),
      };

      var largePurchase = new LargePurchaseScore(listOfPurchases, 1750); //87.5

      var score = largePurchase.Score;

      Assert.Equal(750, score);
    }


    [Fact]
    public void CalculateLargePurchase_insideRange()
    {


      var listOfPurchases = new List<CreditEngine.Transaction>()
      {
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays( -5), 90),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-10), 85),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-15), 80),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-20), 75),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-25), 70),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-30), 65),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-35), 60),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-40), 55),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-45), 50),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-50), 45),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-55), 40),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-60), 35),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-65), 30),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-70), 25),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-75), 20),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-80), 15),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-85), 10),
        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-90),  5),

        new CreditEngine.Transaction(DateTimeOffset.UtcNow.AddDays(-119),  500),
      };

      var largePurchase = new LargePurchaseScore(listOfPurchases, 1750); //87.5

      var score = largePurchase.Score;

      Assert.Equal(500, score);
    }

    #endregion

  }
}