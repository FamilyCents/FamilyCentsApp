using FamilyCents.App.Data.Models;
using Newtonsoft.Json;
using System;
using Xunit;

namespace FamilyCents.App.Tests
{
  public class JsonConversionTests
  {
    [Fact]
    public void ConvertsDateToEpoch()
    {
      var json = @"{
  ""date"": 1509810667993
}";

      var deserialized = JsonConvert.DeserializeObject<CustomerApiResponse>(json);

      Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1509810667993), deserialized.Date);
    }
  }
}