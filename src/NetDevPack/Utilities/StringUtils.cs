using System;

namespace NetDevPack.Utilities
{
    public static class StringUtils
    {
        private static readonly Random random = new Random();
        public static string RandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var content = new char[length];
            for (int i = 0; i < length; i++)
            {
                content[i] = chars[random.Next(chars.Length)];
            }

            return new string(content);
        }
    }
}
