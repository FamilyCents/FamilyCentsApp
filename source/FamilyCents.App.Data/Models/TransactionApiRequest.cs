using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class TransactionApiRequest
  {
    [JsonProperty("Date_from")]
    public DateTimeOffset? DateFrom { get; set; }
    [JsonProperty("Date_to")]
    public DateTimeOffset? DateTo { get; set; }
    [JsonProperty("min_amount")]
    public decimal? MinAmount { get; set; }
    [JsonProperty("max_amount")]
    public decimal? MaxAmount { get; set; }
    [JsonProperty("card_number")]
    public string CardAmount { get; set; }
    [JsonProperty("account_id")]
    public int? AccountId { get; set; }
    [JsonProperty("customer_id")]
    public int? CustomerId { get; set; }
    [JsonProperty("merchant_name")]
    public string MerchantName { get; set; }
    [JsonProperty("card_type")]
    public string CardType { get; set; }
    [JsonProperty("transaction_zipcode")]
    public string TransactionZipcode { get; set; }
  }
}
