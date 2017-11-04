using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class AccountApiRequest
  {
    [JsonProperty("account_id")]
    public int AccountId { get; set; }
    [JsonProperty("card_type")]
    public string CardType { get; set; }
  }
}
