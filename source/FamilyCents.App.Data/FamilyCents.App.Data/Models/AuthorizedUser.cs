using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class AuthorizedUser
  {
    [JsonConstructor]
    internal AuthorizedUser() { }

    [JsonProperty("customer_id")]
    public string CustomerId { get; internal set; }
    [JsonProperty("au_card_number")]
    public string CardNumber { get; internal set; }
  }
}
