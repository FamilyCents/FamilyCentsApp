using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyCents.App.Data.Models
{
  public sealed class FamilyTask : IAffectsBalance
  {
    [JsonProperty]
    public int AccountId { get; internal set; }
    [JsonProperty]
    public int CreatedBy { get; internal set; }
    [JsonProperty("id")]
    public Guid TaskId { get; internal set; }
    [JsonProperty]
    public string Description { get; internal set; }
    [JsonProperty]
    public decimal Value { get; internal set; }
    [JsonProperty]
    public DateTimeOffset WhenCreated { get; internal set; }
    [JsonProperty]
    public DateTimeOffset? WhenCompleted { get; internal set; }
    [JsonProperty]
    public int? CompletedBy { get; internal set; }
    [JsonProperty]
    public DateTimeOffset? WhenApproved { get; internal set; }
    [JsonProperty]
    public int? ApprovedBy { get; internal set; }

    [JsonIgnore]
    decimal IAffectsBalance.Amount => -Value;

    internal FamilyTask Clone() => MemberwiseClone() as FamilyTask;
  }
}
