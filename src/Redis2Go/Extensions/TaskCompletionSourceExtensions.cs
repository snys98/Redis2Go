using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redis2Go.Extensions
{
    public static class TaskCompletionSourceExtensions
    {
        public static TaskCompletionSource<T> Timeout<T>(this TaskCompletionSource<T> @this, int timeoutMilliseconds = 1000)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(timeoutMilliseconds));
            cts.Token.Register(() => @this.TrySetException(new TimeoutException()));

            return @this;
            
        }
    }
}
