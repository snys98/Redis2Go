using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Redis2Go.Tests
{
    public class RedisRunnerTest
    {
        [Fact]
        public async Task ShouldRunAsync()
        {
            //Act
            using (var actual = await RedisRunner.StartAsync())
            {
                //Assert
                Assert.Equal(State.Running, actual.State);
            }
        }

        [Fact]
        public async Task ShouldTimeoutAsync()
        {
            //Act
            var exception = await Assert.ThrowsAsync<TimeoutException>(
                async ()=> await RedisRunner.StartAsync(1)
                );

            //Assert
            Assert.NotNull(exception);
        }
    }
}
