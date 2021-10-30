using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace NetDevPack.Perf.Tests
{
    [MemoryDiagnoser, RPlotExporter]
    public class RandomStringComparison
    {
        private static Random random = new Random();
        private const int Length = 10;

        [Benchmark]
        public string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, Length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Benchmark]
        public string NetDevPackRandomString() => Utilities.StringUtils.RandomString(Length);
    }
}
