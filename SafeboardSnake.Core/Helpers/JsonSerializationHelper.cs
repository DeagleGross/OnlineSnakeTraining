using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SafeboardSnake.Core.Helpers
{
    public static class JsonSerializationHelper
    {
        private static readonly JsonSerializerOptions JsonOptionsWithoutCaseSensitivity = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public static string JsonSerialize<T>(this T item)
        {
            return JsonSerializer.Serialize(item, JsonOptionsWithoutCaseSensitivity);
        }

        public static T JsonDeserialize<T>(this string str)
        {
            return JsonSerializer.Deserialize<T>(str, JsonOptionsWithoutCaseSensitivity);
        }

    }
}
