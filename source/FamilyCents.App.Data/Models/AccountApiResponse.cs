using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class AccountApiResponse
  {
    [JsonConstructor]
    internal AccountApiResponse() { }

    [JsonProperty("account_id")]
    public int AccountId { get; internal set; }
    [JsonProperty("primary_customer_id")]
    public int PrimaryCustomerId { get; internal set; }
    [JsonProperty("au")]
    public ImmutableList<AuthorizedUser> AuthorizedUsers { get; internal set; }
    [JsonProperty("credit_limit")]
    public int CreditLimit { get; internal set; }
    [JsonProperty("balance")]
    public int Balance { get; internal set; }
    [JsonProperty("card_type")]
    public string CardType { get; internal set; }
    [JsonProperty("card_number")]
    public string CardNumber { get; internal set; }
    [JsonProperty("total_rewards_earned")]
    public int TotalRewardsEarned { get; internal set; }
    [JsonProperty("total_rewards_used")]
    public int TotalRewardsUsed { get; internal set; }
  }
}
