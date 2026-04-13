using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Sampaio.Shared.Utils
{
    public static class JsonUtils
    {
        public static JsonSerializerSettings GetSettings(bool indent = false)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = indent ? Formatting.Indented : Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            };
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }

        public static T Deserialize<T>(string json) =>
            JsonConvert.DeserializeObject<T>(json, GetSettings());

        public static T DeserializeIgnoreRoot<T>(string json)
        {
            var data = JObject.Parse(json)?.Properties()?.FirstOrDefault()?.Value;
            
            return data == null ? default(T) : Deserialize<T>(Serialize(data));
        }
        
        public static string Serialize(object value, bool indent = false, string root = null)
        {
            if(!string.IsNullOrEmpty(root))
            {               
                var data = new System.Dynamic.ExpandoObject() as IDictionary<string, object>; ;
                data.Add(root, value);
                return JsonConvert.SerializeObject(data, GetSettings(indent));
            }

            return JsonConvert.SerializeObject(value, GetSettings(indent));
        }

        public static string Serialize<T>(T value, bool indent = false, string root = null)
        {
            if (!string.IsNullOrEmpty(root))
            {
                var data = new System.Dynamic.ExpandoObject() as IDictionary<string, object>; ;
                data.Add(root, value);
                return JsonConvert.SerializeObject(data, GetSettings(indent));
            }

            
            return JsonConvert.SerializeObject(value, GetSettings(indent));
        }

        public static T CastObject<T>(object obj)
        {
            var jsonSeralizer = new JsonSerializer { ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore };
            var data = ((JObject)obj).ToObject<T>(jsonSeralizer);
            return data;
        }

        public static JsonTextWriter GetJsonTextWriter(StringWriter sw)
        {   
            JsonTextWriter writer = new JsonTextWriter(sw);
            writer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            writer.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
            writer.Formatting = Formatting.None;           
            return writer;
        }
    }
    
    public class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
    
    public class PrivateSetterCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
                return jProperty;

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }

    internal static class MemberInfoExtensions
    {
        internal static bool IsPropertyWithSetter(this MemberInfo member)
        {
            var property = member as PropertyInfo;

            return property?.GetSetMethod(true) != null;
        }
    }
}
