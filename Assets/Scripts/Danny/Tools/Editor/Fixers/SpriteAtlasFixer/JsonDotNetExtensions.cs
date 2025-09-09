using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SupportUtils
{
    public static class JsonDotNetExtensions
    {
        public static object ToObject(this JToken token)
        {
            if (token is JValue)
                return ((JValue) token).Value;
            if (token is JArray)
                return token.AsEnumerable().Select(ToObject).ToList();
            if (token is JObject)
                return token.AsEnumerable().Cast<JProperty>()
                    .ToDictionary(x => x.Name, x => ToObject(x.Value));
            throw new InvalidOperationException("Unexpected token: " + token);
        }
    }
}