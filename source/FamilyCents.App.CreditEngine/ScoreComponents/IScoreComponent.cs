using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.CreditEngine.ScoreComponents
{
  public interface IScoreComponent
  {
    double Score { get; }
  }
}
