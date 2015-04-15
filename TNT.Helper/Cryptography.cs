using System.Text;
using System.IO;
using System.Security.Cryptography;
using System;
using System.Web;

namespace TNTHelper
{
    public class Cryptography
    {
        protected static byte[] KeyB = {
		55,
		12,
		45,
		251,
		84,
		92,
		194,
		134,
		190,
		120,
		88,
		163,
		58,
		129,
		129,
		90,
		20,
		45,
		121,
		199,
		128,
		98,
		3,
		50,
		89,
		155,
		84,
		187,
		222,
		160,
		171,
		74
	};
        protected static byte[] IvB = {
		11,
		75,
		235,
		125,
		120,
		88,
		79,
		255,
		13,
		181,
		197,
		212,
		55,
		46,
		56,
		85

	};
        public static string EncryptString(string src)
        {
            return EncryptString(src, false);
        }

        public static string EncryptString(string src, bool ForWeb)
        {

            UTF8Encoding TextConverter = new UTF8Encoding();
            byte[] p = TextConverter.GetBytes(src);
            byte[] encodedBytes = null;

            MemoryStream ms = new MemoryStream();
            RijndaelManaged rv = new RijndaelManaged();
            CryptoStream cs_base64 = new CryptoStream(ms, new ToBase64Transform(), CryptoStreamMode.Write);
            CryptoStream cs = new CryptoStream(cs_base64, rv.CreateEncryptor(KeyB, IvB), CryptoStreamMode.Write);

            cs.Write(p, 0, p.Length);
            cs.FlushFinalBlock();
            encodedBytes = ms.ToArray();
            ms.Close();
            cs.Close();

            string ret = TextConverter.GetString(encodedBytes);

            if (ForWeb)
            {
                ret = HttpUtility.UrlEncode(ret);
            }

            return ret;

        }

        public static string DecryptString(string value)
        {
            return DecryptString(value, false);
        }

        public static string DecryptString(string value, bool forWeb)
        {

            string ret = value;

            if (forWeb)
            {
                ret = System.Web.HttpUtility.UrlDecode(ret);
            }

            UTF8Encoding TextConverter = new UTF8Encoding();
            byte[] p = TextConverter.GetBytes(ret.ToCharArray());
            byte[] EncodedBytes = null;

            RijndaelManaged rv = new RijndaelManaged();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs_tmp = new CryptoStream(ms, rv.CreateDecryptor(KeyB, IvB), CryptoStreamMode.Write);
            CryptoStream cs = new CryptoStream(cs_tmp, new FromBase64Transform(), CryptoStreamMode.Write);

            cs.Write(p, 0, p.Length);
            cs.Close();

            EncodedBytes = ms.ToArray();
            ret = TextConverter.GetString(EncodedBytes);

            return ret;

        }

        public static string EncryptBase64(string scr) {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(scr));
        }

        public static string DecryptBase64(string scr) {
           return Encoding.UTF8.GetString(Convert.FromBase64String(scr));
        }

    }
}
