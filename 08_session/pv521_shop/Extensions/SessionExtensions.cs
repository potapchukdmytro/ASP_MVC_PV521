using System.Text.Json;

namespace _01_intro.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            session.SetString(key, json);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
