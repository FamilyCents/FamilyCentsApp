using Newtonsoft.Json;
using System;

namespace FamilyCents.App.Data.Models
{
  public sealed class TransactionApiResponse
  {
    [JsonConstructor]
    internal TransactionApiResponse() { }

    [JsonProperty("card_type")]
    public string CardType { get; internal set; }
    [JsonProperty("account_id")]
    public int AccountId { get; internal set; }
    [JsonProperty("customer_id")]
    public int CustomerId { get; internal set; }
    [JsonProperty("first_name")]
    public string FirstName { get; internal set; }
    [JsonProperty("last_name")]
    public string LastName { get; internal set; }
    [JsonProperty("customer_zipcode")]
    public string CustomerZipcode { get; internal set; }
    [JsonProperty("gender")]
    public string Gender { get; internal set; }
    [JsonProperty("is_married")]
    public bool IsMarried { get; internal set; }
    [JsonProperty("e_mail")]
    public string Email { get; internal set; }
    [JsonProperty("card_number")]
    public string CardNumber { get; internal set; }
  }
}
