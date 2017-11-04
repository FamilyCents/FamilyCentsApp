using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FamilyCents.App.Data.Models
{
  internal sealed class EpochDateConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType) => objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      var isNullable = objectType == typeof(DateTimeOffset?);

      if (isNullable && reader.Value == null)
      {
        return null as DateTimeOffset?;
      }

      return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(reader.Value));
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      if (value == null)
      {
        writer.WriteNull();
      }
      else
      {
        writer.WriteRawValue(((DateTimeOffset)value).ToUnixTimeSeconds().ToString());
      }
    }
  }
}
