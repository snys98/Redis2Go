using System;
using System.IO;

namespace Redis2Go
{
    public enum RedisTypes
    {
        Redis64,
        Redis32
    }

    public static class RedisTypesExtensions
    {
        private static readonly string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string Redis64 = $@"{CurrentDirectory}\tools\MemuraiDeveloper\";
        private static readonly string Redis32 = $@"{CurrentDirectory}\tools\MemuraiDeveloper\";

        public static string GetBinaryPath(this RedisTypes @this)
        {
            switch (@this)
            {
                case RedisTypes.Redis64:return Redis64;
                case RedisTypes.Redis32:return Redis32;
                default:throw new ArgumentOutOfRangeException();
            }
        }
    }
}
