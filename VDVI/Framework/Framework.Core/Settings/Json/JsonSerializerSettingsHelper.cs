using System.Collections.Generic;
using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Core.Settings.Json
{
    public static class JsonSerializerSettingsHelper
    {
        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new CustomDatetimeConverter() },
                ContractResolver = new PrivateSetterContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };

            return jsonSerializerSettings;
        }
    }

    public static class JsonExtensions
    {
        public static bool IsNullOrEmpty(this JToken token)
        {
            return token == null ||
                   token.Type == JTokenType.Array && !token.HasValues ||
                   token.Type == JTokenType.Object && !token.HasValues ||
                   token.Type == JTokenType.String && token.ToString() == string.Empty ||
                   token.Type == JTokenType.Null;
        }
    }
}