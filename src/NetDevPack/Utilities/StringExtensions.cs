using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.String;


namespace NetDevPack.Utilities
{
    public static class StringExtensions
    {
        private static char sensitive = '*';
        private static char at = '@';
        private static readonly Regex UrlizeRegex = new Regex(@"[^A-Za-z0-9_~]+", RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled);

        public static string UrlEncode(this string url)
        {
            return Uri.EscapeDataString(url);
        }

        public static bool NotEqual(this string original, string compareTo)
        {
            return !original.Equals(compareTo);
        }

        public static bool IsEmail(this string field)
        {
            // Return true if strIn is in valid e-mail format.
            return field.IsPresent() && EmailRegex.IsMatch(field);
        }

        public static bool IsMissing(this string value)
        {
            return IsNullOrEmpty(value);
        }

        public static bool IsPresent(this string value)
        {
            return !IsNullOrWhiteSpace(value);
        }

        private static string UrlCombine(string path1, string path2)
        {
            path1 = path1.TrimEnd('/') + "/";
            path2 = path2.TrimStart('/');

            return Path.Combine(path1, path2)
                .Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        public static string UrlPathCombine(this string path1, params string[] path2)
        {
            path1 = path1.TrimEnd('/') + "/";
            foreach (var s in path2)
            {
                path1 = UrlCombine(path1, s).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }

            return path1;

        }

        public static string AddSpacesToSentence(this string state)
        {
            var text = state.ToCharArray();
            var chars = new char[text.Length + HowManyCapitalizedChars(text) - 1];

            chars[0] = text[0];
            int j = 1;
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    if (text[i - 1] != ' ' && !char.IsUpper(text[i - 1]) ||
                        (char.IsUpper(text[i - 1]) && i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    {
                        chars[j++] = ' ';
                        chars[j++] = text[i];
                        continue;
                    }
                }

                chars[j++] = text[i];
            }
#if NETSTANDARD2_0
            return new string(chars);
#else
            return new string(chars.AsSpan());
#endif
        }

        private static int HowManyCapitalizedChars(char[] state)
        {
            var count = 0;
            for (var i = 0; i < state.Length; i++)
            {
                if (char.IsUpper(state[i]))
                    count++;
            }

            return count;
        }

        public static bool HasTrailingSlash(this string url)
        {
            return url != null && url.EndsWith("/");
        }


#if NET5_0_OR_GREATER || NETSTANDARD2_1
        public static string TruncateSensitiveInformation(this string part)
        {
            return part.AsSpan().TruncateSensitiveInformation();
        }

        /// <summary>
        /// Replace everything to ***, except the first and last char
        /// </summary>
        /// <returns></returns>
        public static string TruncateSensitiveInformation(this ReadOnlySpan<char> part)
        {
            var firstAndLastLetterBuffer = new char[2];
            var firstAndLastLetter = new Span<char>(firstAndLastLetterBuffer);

            if (part != Empty)
            {
                part[..1].CopyTo(firstAndLastLetter[..1]);
                part[^1..].CopyTo(firstAndLastLetter[1..]);

                return Create(part.Length, firstAndLastLetterBuffer, (span, s) =>
                {
                    s.AsSpan(0, 1).CopyTo(span);
                    for (var i = 1; i < span.Length - 1; i++)
                    {
                        span[i] = sensitive;
                    }

                    s.AsSpan(s.Length - 1).CopyTo(span[^1..]);
                });
            }
            else
            {
                return Empty;
            }

        }
#else

        /// <summary>
        /// Replace everything to ***, except the first and last char
        /// </summary>
        /// <returns></returns>
        public static string TruncateSensitiveInformation(this string part)
        {

            if (part.IsPresent())
            {
                var truncatedString = new char[part.Length];
                truncatedString[0] = part[0];


                for (var i = 1; i < part.Length - 1; i++)
                {
                    truncatedString[i] = sensitive;
                }
                truncatedString[part.Length - 1] = part[part.Length - 1];

                return new string(truncatedString);
            }

            return string.Empty;
        }
#endif

#if NET5_0_OR_GREATER || NETSTANDARD2_1
        /// <summary>
        /// Truncate e-mail for frontend exibition:
        /// myemail@microsoft.com -> m*****@m***t.com
        /// </summary>
        /// <param name="email"></param>
        public static string TruncateEmail(this string email)
        {
            var beforeAt = email.AsSpan(0, email.IndexOf(at)).TruncateSensitiveInformation().AsSpan();
            var afterAt = email.AsSpan(email.IndexOf(at) + 1).TruncateSensitiveInformation().AsSpan();

            var finalSpan = new Span<char>(new char[email.Length]);

            beforeAt.CopyTo(finalSpan);
            finalSpan[beforeAt.Length] = at;
            afterAt.CopyTo(finalSpan.Slice(beforeAt.Length + 1));

            return finalSpan.ToString();
        }

#endif
        public static string ToSha256(this string value)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }

        /// <summary>
        /// Remove áóé etc.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var finalText = new char[text.Length];
            var lastIndex = 0;
            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    finalText[lastIndex++] = c;
                }
            }
            Array.Resize(ref finalText, lastIndex);

            return new string(finalText).Normalize(NormalizationForm.FormC);

        }

#if NET5_0_OR_GREATER || NETSTANDARD2_1
        /// <summary>
        /// Turn string into URL dashed style.
        /// E.g: My custom url title provided by user -> my-custom-url-title-provided-by-user
        /// Useful to create Url paths and improve SEO.
        /// </summary>
        public static string Urlize(this string str)
        {
            var tituloEditado = str.Trim().ToLower().RemoveDiacritics();

            tituloEditado = UrlizeRegex.Replace(tituloEditado, "-");

            if (tituloEditado.StartsWith('-'))
                tituloEditado = tituloEditado[1..];

            if (tituloEditado.EndsWith('-'))
                tituloEditado = tituloEditado[..^1];

            return tituloEditado;

        }
#else
        /// <summary>
        /// Turn string into URL dashed style.
        /// E.g: My custom url title provided by user -> my-custom-url-title-provided-by-user
        /// Useful to create Url paths and improve SEO.
        /// </summary>
        public static string Urlize(this string str)
        {
            var tituloEditado = str.Trim().ToLower().RemoveDiacritics();

            tituloEditado = UrlizeRegex.Replace(tituloEditado, "-");

            if (tituloEditado.StartsWith("-"))
                tituloEditado = tituloEditado.Substring(1);

            if (tituloEditado.EndsWith("-"))
                tituloEditado = tituloEditado.Substring(0, tituloEditado.Length - 1);

            return tituloEditado;

        }
#endif

        public static string OnlyNumbers(this string str)
        {
            var onlyNumbers = new char[str.Length];
            var lastIndex = 0;

            foreach (var c in str)
            {
                if (c < '0' || c > '9') continue;

                onlyNumbers[lastIndex++] = c;
            }
            Array.Resize(ref onlyNumbers, lastIndex);
            return new string(onlyNumbers);
        }


        public static string FromBase64ToString(this string str, Encoding enc = null)
        {
            return (enc ?? Encoding.Default).GetString(FromBase64(str));
        }

        public static byte[] FromBase64(this string str)
        {
            return Convert.FromBase64String(str);
        }
        public static string ToBase64(this string str, Encoding enc = null)
        {
            return ToBase64((enc ?? Encoding.Default).GetBytes(str));
        }

        public static string ToBase64(this byte[] data, Encoding enc = null)
        {
            return Convert.ToBase64String(data);
        }
        public static byte[] FromPlainHexDumpStyleToByteArray(this string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        private static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        /// <summary>
        /// Equivalent to xxd -p
        /// see: https://linux.die.net/man/1/xxd
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToPlainHexDumpStyle(this byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }

        public static string Capitalize(this string value, bool isRestLower)
        {
            var spanChars = value.AsSpan();
            var newSpan = new Span<char>(new char[value.Length]);
            spanChars.CopyTo(newSpan);

            if (isRestLower)
            {
                spanChars.ToLowerInvariant(newSpan);
            }

            newSpan[0] = char.ToUpper(spanChars[0]);

            return newSpan.ToString();
        }
    }
}