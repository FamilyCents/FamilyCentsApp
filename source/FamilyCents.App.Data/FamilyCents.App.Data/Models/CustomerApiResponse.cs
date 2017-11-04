using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class CustomerApiResponse
  {
    [JsonConstructor]
    internal CustomerApiResponse() { }

    [JsonProperty("card_type")]
    public string CardType { get; internal set; }
    [JsonProperty("account_id")]
    public int AccountId { get; internal set; }
    [JsonProperty("amount")]
    public int Amount { get; internal set; }
    [JsonProperty("merchant")]
    public string Merchant { get; internal set; }
    [JsonProperty("date"), JsonConverter(typeof(EpochDateConverter))]
    public DateTimeOffset Date { get; internal set; }
    [JsonProperty("transaction_zipcode")]
    public string TransactionZipcode { get; internal set; }
    [JsonProperty("customer_id")]
    public int CustomerId { get; internal set; }
    [JsonProperty("rewards_earned")]
    public int RewardsEarned { get; internal set; }
    [JsonProperty("transactions_id")]
    public int TransactionsId { get; internal set; }
  }
}
