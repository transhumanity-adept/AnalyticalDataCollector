using System.Collections.Concurrent;
using System.Diagnostics;

namespace Utils;

// TODO: Доделать
public class Limiter<T, V>
{
    private readonly TimeSpan interval;
    private readonly int maxCallCount;

    private readonly int currentCallCount = 0;
    private readonly Stopwatch watch = new Stopwatch();

    public Limiter(TimeSpan interval, int maxCallCount)
    {
        this.interval = interval;
        this.maxCallCount = maxCallCount;
    }

    public Task<V> ExecuteWithLimit(Func<T, V> function, T paramValue)
    {
        return Task.Run<V>(async () =>
        {
            await Task.Delay();
            return function.Invoke(paramValue);
        });
    }
}