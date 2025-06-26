
using System.Text.Json;

namespace HelloAspMVC.Controllers;
// 세션 처리 
// https://learn.microsoft.com/ko-kr/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0 제공하는 코드
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}