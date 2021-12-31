using BenchmarkDotNet.Running;
using NetDevPack.Perf.Tests;

namespace NetDevPack.Perf
{

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<RandomStringComparison>();
            BenchmarkRunner.Run<OnlyNumbersComparison>();
            BenchmarkRunner.Run<UrlizeComparison>();
            BenchmarkRunner.Run<RemoveDiatricsComparison>();
            BenchmarkRunner.Run<CapitalizeComparison>();
        }
    }
}
