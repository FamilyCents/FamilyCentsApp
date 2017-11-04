using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class AccountCustomer
  {
    [JsonConstructor]
    internal AccountCustomer() { }

    [JsonProperty("account_id")]
    public int AccountId { get; internal set; }
    [JsonProperty("customers")]
    public ImmutableList<Customer> Customers { get; internal set; }
  }
}
