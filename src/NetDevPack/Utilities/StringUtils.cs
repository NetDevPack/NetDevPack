using System.Linq;
using System.Text.RegularExpressions;

namespace NetDevPack.Utilities
{
    public static class StringUtils
    {
        public static string OnlyNumbers(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9-_.]+", "", RegexOptions.Compiled);
        }
    }
}
