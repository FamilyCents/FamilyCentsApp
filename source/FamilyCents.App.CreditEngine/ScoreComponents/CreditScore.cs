using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
    public class CreditScore
    {



    public ConsumptionScore ConsumptionRate { get; private set; }
    public LargePurchaseScore LargePurchases { get; private set; }
    public LifespanScore Lifespan { get; private set; }
    public PaymentScore Payments { get; private set; }
    public UtilizationScore Utilization { get; private set; }


    public int Score { get => ConsumptionRate.Score + LargePurchases.Score + Lifespan.Score +
                              Payments.Score + Utilization.Score;
    }


  }
}
