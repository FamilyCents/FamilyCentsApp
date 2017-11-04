using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class PaymentApiResponse
  {
    [JsonConstructor]
    internal PaymentApiResponse() { }

    [JsonProperty("account_id")]
    public int AccountId { get; internal set; }
    [JsonProperty("payment")]
    public Payment Payment { get; internal set; }
  }
}
