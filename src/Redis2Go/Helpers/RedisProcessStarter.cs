using Redis2Go.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Redis2Go.Helpers
{
    public class RedisProcessStarter : IRedisProcessStarter
    {
        /// <summary>
        /// Starts a new process.
        /// </summary>
        public IRedisProcess Start(string binariesDirectory, int port = RedisDefaults.DefaultPort)
        {
            string fileName = string.Format(@"{0}\{1}", binariesDirectory, RedisDefaults.RedisExecutable);
            string arguments = string.Format(@"--port {0}", port);
            List<string> errorOutput = new List<string>();
            List<string> standardOutput = new List<string>();
            Process redisServerProcess = null;
            try
            {
                redisServerProcess = Process.Start(new ProcessStartInfo(fileName, arguments));
                standardOutput.Add("redis-server started on successfully");
            }
            catch (Exception ex)
            {
                errorOutput.Add(string.Format("Cound not start redis-server.  Error: {0}", ex.Message));
            }

            RedisProcess redisProcess = new RedisProcess(redisServerProcess)
            {
                ErrorOutput = errorOutput,
                StandardOutput = standardOutput
            };

            return redisProcess;
        }

        /// <summary>
        /// Starts a new process with awaiter.
        /// </summary>
        public Task<IRedisProcess> StartAsync(string binariesDirectory, int port = RedisDefaults.DefaultPort, int timeoutMilliseconds = 5000)
        {
            var initializeWaiter = new TaskCompletionSource<IRedisProcess>().Timeout(timeoutMilliseconds);

            string fileName = string.Format(@"{0}\{1}", binariesDirectory, RedisDefaults.RedisExecutable);
            string arguments = string.Format(@"--port {0}", port);

            List<string> errorOutput = new List<string>();
            List<string> standardOutput = new List<string>();

            Process redisServerProcess = null;
            try
            {
                redisServerProcess = Process.Start(new ProcessStartInfo(fileName, arguments) {
                    RedirectStandardOutput = true
                });
            }
            catch (Exception ex)
            {
                redisServerProcess?.Kill();
                initializeWaiter.SetException(
                    new Exception(string.Format("Cound not start redis-server.  Error: {0}", ex.Message)));
            }

            redisServerProcess.OutputDataReceived += (sender, eventArg) =>
            {
                try
                {
                    if (IsInitializeSussess(eventArg))
                    {
                        standardOutput.Add("redis-server started on successfully");

                        RedisProcess redisProcess = new RedisProcess(redisServerProcess)
                        {
                            ErrorOutput = errorOutput,
                            StandardOutput = standardOutput
                        };

                        initializeWaiter.SetResult(redisProcess);
                    }
                }
                catch
                {
                    redisServerProcess?.Kill();
                }
            };

            redisServerProcess.BeginOutputReadLine();

            return initializeWaiter.Task;
        }

        private static bool IsInitializeSussess(DataReceivedEventArgs eventArg)
        {
            return !string.IsNullOrEmpty(eventArg.Data)
                   && eventArg.Data.Contains("The server is now ready to accept connections");
        }
    }
}
