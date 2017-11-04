using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class Customer : ICustomerScoped, IEquatable<Customer>
  {
    [JsonConstructor]
    internal Customer() { }

    [JsonProperty("customer_id")]
    public int CustomerId { get; internal set; }
    [JsonProperty("first_name")]
    public string FirstName { get; internal set; }
    [JsonProperty("last_name")]
    public string LastName { get; internal set; }
    [JsonProperty("zipcode")]
    public string Zipcode { get; internal set; }
    [JsonProperty("dob")]
    public DateTime DateOfBirth { get; internal set; }
    [JsonProperty("gender")]
    public string Gender { get; internal set; }
    [JsonProperty("is_married")]
    public bool IsMarried { get; internal set; }
    [JsonProperty("email")]
    public string Email { get; internal set; }
    [JsonProperty("credit_card_number")]
    public string CardNumber { get; internal set; }
    [JsonProperty("country")]
    public string Country { get; internal set; }

    public bool Equals(Customer other) => other != null && other.CustomerId == CustomerId;
    public override int GetHashCode() => CustomerId.GetHashCode();
    public override bool Equals(object obj) => obj is Customer customer && Equals(customer);
  }
}
