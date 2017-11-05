using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class CustomerCreditLimit : ICustomerScoped
  {
    [JsonProperty]
    public decimal Current { get; internal set; }
    [JsonProperty]
    public decimal Max { get; internal set; }
    [JsonProperty]
    public decimal Min { get; internal set; }
    [JsonProperty]
    public decimal AccountId { get; internal set; }
    [JsonProperty]
    public int CustomerId { get; internal set; }
    [JsonProperty("id")]
    public Guid CustomerCreditLimitId { get; internal set; }

    internal CustomerCreditLimit Clone() => MemberwiseClone() as CustomerCreditLimit;
  }
}
