using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class PaymentApiRequest
  {
    [JsonProperty("account_id")]
    public int AccountId { get; set; }
    [JsonProperty("month_year_from"), JsonConverter(typeof(EpochDateConverter))]
    public DateTimeOffset From { get; set; }
    [JsonProperty("month_year_to"), JsonConverter(typeof(EpochDateConverter))]
    public DateTimeOffset To { get; set; }
  }
}
