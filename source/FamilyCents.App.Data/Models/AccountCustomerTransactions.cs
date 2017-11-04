using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class AccountCustomerTransactions
  {
    [JsonConstructor]
    internal AccountCustomerTransactions() { }
    [JsonProperty("account_id")]
    public int AccountId { get; internal set; }
    [JsonProperty("customers")]
    public ImmutableList<AccountCustomerTransactions> CustomerTransactions { get; internal set; }
  }
}
