using System;

namespace Redis2Go
{
    public enum RedisTypes
    {
        Redis64,
        Redis32
    }

    public static class RedisTypesExtensions
    {
        private static readonly string Redis64 = @"tools\Redis32\";
        private static readonly string Redis32 = @"tools\Redis64\";

        public static string GetBinaryPath(this RedisTypes @this)
        {
            var systemType = Environment.Is64BitOperatingSystem;

            switch (@this)
            {
                case RedisTypes.Redis64:return Redis64;
                case RedisTypes.Redis32:return Redis32;
                default:throw new ArgumentOutOfRangeException();
            }
        }
    }
}
