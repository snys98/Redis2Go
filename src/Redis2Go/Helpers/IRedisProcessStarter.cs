
using System.Threading.Tasks;

namespace Redis2Go.Helpers
{
    public interface IRedisProcessStarter
    {
        IRedisProcess Start(string binariesDirectory, int port);
        Task<IRedisProcess> StartAsync(string binariesDirectory, int port, int timeoutMilliseconds = 5000);
    }
}
