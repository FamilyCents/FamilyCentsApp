using Newtonsoft.Json;
using System;

namespace FamilyCents.App.Data.Models
{
  public sealed class Transaction : IDayAccurateDate
  {
    [JsonConstructor]
    internal Transaction() { }

    [JsonProperty("transaction_row_id")]
    public int TransactionRowId { get; internal set; }
    [JsonProperty("zipcode")]
    public string Zipcode { get; internal set; }
    [JsonProperty("month")]
    public Month Month { get; internal set; }
    [JsonProperty("amount")]
    public decimal Amount { get; internal set; }
    [JsonProperty("year")]
    public int Year { get; internal set; }
    [JsonProperty("country")]
    public string Country { get; internal set; }
    [JsonProperty("day")]
    public int Day { get; internal set; }
    [JsonProperty("merchant_name")]
    public string MerchantName { get; internal set; }
    [JsonProperty("transaction_id")]
    public int TransactionId { get; internal set; }
  }
}