using FamilyCents.App.CreditEngine.ScoreComponents;
using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using Xunit;

namespace FamilyCents.App.Tests
{
  public class CreditScoreTests
  {
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
  }
}