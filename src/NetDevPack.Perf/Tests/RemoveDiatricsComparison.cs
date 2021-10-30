using BenchmarkDotNet.Attributes;
using NetDevPack.Utilities;
using System;
using System.Globalization;
using System.Text;

namespace NetDevPack.Perf.Tests
{
    [MemoryDiagnoser, RPlotExporter]
    public class RemoveDiatricsComparison
    {
        private const string Text = "Aáoóeéuúaâoôuû";

        [Benchmark]
        public string RemoveDiacritics()
        {

            var normalizedString = Text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        [Benchmark]
        public string NetDevPackRemoveDiacritics() => Text.RemoveDiacritics();
    }
}
