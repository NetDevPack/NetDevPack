using BenchmarkDotNet.Attributes;
using NetDevPack.Utilities;
using System;

namespace NetDevPack.Perf.Tests
{
    [MemoryDiagnoser, RPlotExporter]
    public class OnlyNumbersComparison
    {
        private const string Data = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [Benchmark]
        public string OnlyNumbers()
        {
            var onlyNumbers = new char[5];
            var lastIndex = 0;

            foreach (var c in Data)
            {
                if (c < '0' || c > '9') continue;

                if (onlyNumbers.Length == lastIndex)
                    Array.Resize<char>(ref onlyNumbers, lastIndex + 5);

                onlyNumbers[lastIndex++] = c;
            }
            Array.Resize(ref onlyNumbers, lastIndex);
            return new string(onlyNumbers);
        }

        [Benchmark]
        public string NetDevPackOnlyNumber() => Data.OnlyNumbers();

    }
}
