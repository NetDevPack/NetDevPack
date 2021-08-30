using System;
using System.Globalization;

namespace NetDevPack.Utilities
{
    public class AspNetIdentityHashInfo
    {
        public AspNetIdentityHashInfo(string base64Hash)
        {
            HexHash = base64Hash.FromBase64().ToPlainHexDumpStyle();
            Hash = base64Hash;
            var hashVersion = HexHash.Substring(0, 2);
            switch (hashVersion)
            {
                case "01":
                    HashVersion = AspNetIdentityHashVersion.PBKDF2_HMAC_SHA256;
                    GetV3Info();
                    break;
                case "00":
                    HashVersion = AspNetIdentityHashVersion.PBKDF2_HMAC_SHA1;
                    break;
                default:
                    throw new Exception("Invalid hash version");
            }


        }


        private void GetV3Info()
        {
            HexPrf = HexHash.Substring(2, 8);
            HexIterCount = HexHash.Substring(10, 8);
            HexSaltLength = HexHash.Substring(18, 8);
            HexSalt = HexHash.Substring(26, 32);
            HexSubKey = HexHash.Substring(58, 64);
            IterCount = int.Parse(HexIterCount, NumberStyles.HexNumber);
            SaltLength = int.Parse(HexSaltLength, NumberStyles.HexNumber) * 8;
            Salt = HexSalt.FromPlainHexDumpStyleToByteArray().ToBase64();
            SubKey = HexSubKey.FromPlainHexDumpStyleToByteArray().ToBase64();
            HashcatFormat = $"sha256:{IterCount}:{Salt}:{SubKey}";
            ShaType = "sha256";
        }

        public string ShaType { get; set; }

        public string HashcatFormat { get; set; }

        public string SubKey { get; set; }

        public string Salt { get; set; }

        public int SaltLength { get; set; }

        public int IterCount { get; set; }

        public string HexSubKey { get; set; }

        public string HexSalt { get; set; }

        public string HexSaltLength { get; set; }

        public string HexIterCount { get; set; }

        public string HexPrf { get; set; }

        public AspNetIdentityHashVersion HashVersion { get; set; }
        public string HexHash { get; set; }
        public string Hash { get; set; }
    }
}