using System;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;

public class NestedObjectConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(string);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var jsonString = value as string;
        if (jsonString != null)
        {
            var jsonObject = JObject.Parse(jsonString);
            jsonObject.WriteTo(writer);
        }
        else
        {
            writer.WriteNull();
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // if(existingValue == null)
        // {
        //     return string.Empty;
        // }
        
        // We may switch --- THIS PART 
        // if (reader.TokenType == JsonToken.String)
        // {
        //     var jsonString = (string)reader.Value;
        //     if (jsonString != null) return JObject.Parse(jsonString).ToString();
        // }
        // throw new JsonReaderException("Expected a JSON string");
        // --- END OF PART
        try
        {
            var jsonObject = JObject.Load(reader);
            return jsonObject.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Nested Object Converter Error: {e.GetType()}, {e.Message}");;
            return null;
        }
    }
}