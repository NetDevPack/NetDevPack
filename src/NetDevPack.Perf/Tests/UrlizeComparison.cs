using BenchmarkDotNet.Attributes;
using NetDevPack.Utilities;
using System.Text.RegularExpressions;

namespace NetDevPack.Perf.Tests
{

    [MemoryDiagnoser, RPlotExporter]
    public class UrlizeComparison
    {
        private static readonly Regex UrlizeRegex = new Regex(@"[^A-Za-z0-9_\/~]+", RegexOptions.Multiline | RegexOptions.Compiled);
        private const string Title = " Tipo de dado gerado pela configuração HasColumnType<string> ";

        [Benchmark]
        public string Urlize()
        {
            var editedTitle = Title.Trim().ToLower().RemoveDiacritics();

            editedTitle = UrlizeRegex.Replace(editedTitle, "-");

            if (editedTitle[0] == '-')
                editedTitle = editedTitle[1..];

            if (editedTitle[editedTitle.Length - 1] == '-')
                editedTitle = editedTitle[..^1];

            return editedTitle;
        }

        [Benchmark]
        public string NetDevPackUrlize() => " Tipo de dado gerado pela configuração HasColumnType<string> ".Urlize();
    }
}
