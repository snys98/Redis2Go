# Redis2Go.Alpha
## Forked from https://github.com/Joshscorp/Redis2Go
[![NuGet](https://img.shields.io/nuget/v/Redis2Go.Alpha.svg?maxAge=259200&style=flat)](http://www.nuget.org/packages/Redis2Go.Alpha/)
[![AppVoyer](https://ci.appveyor.com/api/projects/status/github/nickchan9394/redis2go?branch=master&svg=true&passingText=master%20-%20OK)](https://ci.appveyor.com/project/nickchan9394/redis2go/branch/master)

## What's New:
* Release 1.0.0.3
	* Add **_RedisRunner.StartAsync_** for redis server start up to detect the server is actually ready for connection. The parameter is a timeout setting.
	* Include **_64-bit_** and **_32-bit_** redis-server executables in package, which will be automatically selected to launch according to the testing environment.

## Sample usage

#### NUnit:
```
public class TestClass
{
    private readonly RedisRunner _runner;

    [OneTimeSetUp]
    public async Task TestSetUp()
    {
        this._runner = await RedisRunner.StartAsync(); // Default time out is 5 seconds.
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        this._runner.Dispose();
    }
}
```    
