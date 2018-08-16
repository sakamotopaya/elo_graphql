
using Newtonsoft.Json;

namespace Elo {
    public static class SerializationUtilities {
        public static string ToJson(this object model) {
            return JsonConvert.SerializeObject(model);
        }

        public static T ToObject<T>(this string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}