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

#if NET5_0_OR_GREATER || NETSTANDARD2_1
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
#else
        [Benchmark]
        public string Urlize()
        {
            var editedTitle = Title.Trim().ToLower().RemoveDiacritics();

            editedTitle = UrlizeRegex.Replace(tituloEditado, "-");

            if (editedTitle[0] == "-")
                editedTitle = editedTitle.Substring(1);

            if (editedTitle[editedTitle.Length - 1] == "-")
                editedTitle = editedTitle.Substring(0, editedTitle.Length - 1);

            return editedTitle;
        }
#endif

        [Benchmark]
        public string NetDevPackUrlize() => " Tipo de dado gerado pela configuração HasColumnType<string> ".Urlize();
    }
}
