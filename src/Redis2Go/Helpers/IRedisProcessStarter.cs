
using System.Threading.Tasks;

namespace Redis2Go.Helpers
{
    public interface IRedisProcessStarter
    {
        IRedisProcess Start(int port);
        Task<IRedisProcess> StartAsync(int port, int timeoutMilliseconds = 5000);
    }
}
