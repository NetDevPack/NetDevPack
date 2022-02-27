using BenchmarkDotNet.Attributes;
using NetDevPack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDevPack.Perf.Tests
{
    [MemoryDiagnoser, RPlotExporter]
    public class CapitalizeComparison
    {
        private const string Text = "you Shall Not Pass";

        [Benchmark]
        public string Capitalize()
        {
            return char.ToUpper(Text.First()) + Text.Substring(1).ToLower();
        }

        [Benchmark]
        public string NetDevPackCapitalize() => Text.Capitalize(true);
    }
}
