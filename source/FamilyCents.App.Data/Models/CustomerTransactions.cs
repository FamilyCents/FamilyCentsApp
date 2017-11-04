using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class CustomerTransactions : ICustomerScoped
  {
    [JsonConstructor]
    internal CustomerTransactions() { }

    [JsonProperty("customer_id")]
    public int CustomerId { get; internal set; }
    [JsonProperty("transactions")]
    public ImmutableList<Transaction> Transactions { get; internal set; }

  }
}
