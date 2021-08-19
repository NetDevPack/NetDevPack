using BenchmarkDotNet.Attributes;
using NetDevPack.Utilities;

namespace NetDevPack.Perf.Tests
{

    [MemoryDiagnoser, RPlotExporter]
    public class Urlize
    {
        [Benchmark]
        public string NetDevPackUrlise() => " Tipo de dado gerado pela configuração HasCloumnType<string> ".Urlize();
    }
}
