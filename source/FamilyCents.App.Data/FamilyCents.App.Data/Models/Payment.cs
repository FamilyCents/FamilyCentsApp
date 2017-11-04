using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class Payment
  {
    [JsonConstructor]
    internal Payment() { }

    [JsonProperty("total_balance")]
    public int TotalBalance { get; internal set; }
    [JsonProperty("total_balance_paid")]
    public int TotalBalancePaid { get; internal set; }
    [JsonProperty("total_balance_remaining")]
    public int TotalBalanceRemaining { get; internal set; }
    [JsonProperty("month")]
    public string Month { get; internal set; }
    [JsonProperty("year")]
    public int Year { get; internal set; }
  }
}
