using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class AccountPayment : IAccountScoped
  {
    [JsonConstructor]
    internal AccountPayment() { }

    [JsonProperty("account_id")]
    public int AccountId { get; internal set; }
    [JsonProperty("card_type")]
    public string CardType { get; internal set; }
    [JsonProperty("payments")]
    public ImmutableList<Payment> Payment { get; internal set; }
  }
}
