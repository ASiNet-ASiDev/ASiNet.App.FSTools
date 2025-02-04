#pragma warning disable IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
namespace ASiNet.App.FSTools;
#pragma warning restore IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
static class Extensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var element in source)
            action(element);
    }
}
