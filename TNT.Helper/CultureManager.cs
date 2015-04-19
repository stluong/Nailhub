using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TNTHelper
{
    public static class CultureManager
    {
        const string VietNameseCultureName = "vi";
        const string EnglishCultureName = "en";
        //This matches the format xx or xx-xx 
        // where x is any alpha character, case insensitive
        //The router will determine if it is a supported language
        static Regex _cultureRegexPattern = new Regex(@"^([a-zA-Z]{2})(-[a-zA-Z]{2})?$", RegexOptions.IgnoreCase & RegexOptions.Compiled);
        static Dictionary<string, CultureInfo> SupportedCultures { get; set; }

        #region Private

        static CultureManager()
        {
            InitializeSupportedCultures();
        }
        static CultureInfo DefaultCulture
        {
            get
            {
                return SupportedCultures[EnglishCultureName];
            }
        }
        static void AddSupportedCulture(string name)
        {
            SupportedCultures.Add(name, CultureInfo.CreateSpecificCulture(name));
        }
        static void InitializeSupportedCultures()
        {
            SupportedCultures = new Dictionary<string, CultureInfo>();
            AddSupportedCulture(VietNameseCultureName);
            AddSupportedCulture(EnglishCultureName);
        }
        static string ConvertToShortForm(string code)
        {
            return code.Substring(0, 2);
        }
        static bool CultureIsSupported(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;
            code = code.ToLowerInvariant();
            if (code.Length == 2)
                return SupportedCultures.ContainsKey(code);
            return IsFormattedAsCulture(code) && SupportedCultures.ContainsKey(ConvertToShortForm(code));
        }

        static CultureInfo GetCulture(string code)
        {
            if (!CultureIsSupported(code))
                return DefaultCulture;
            string shortForm = ConvertToShortForm(code).ToLowerInvariant(); ;
            return SupportedCultures[shortForm];
        }

        public static bool IsFormattedAsCulture(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;
            return _cultureRegexPattern.IsMatch(code);

        }

        #endregion
        

        public static string GetLanguage(string code)
        {
            if (!CultureIsSupported(code))
                return DefaultCulture.TwoLetterISOLanguageName;

            string shortForm = ConvertToShortForm(code).ToLowerInvariant();
            return shortForm;
        }

        public static string GetLanguage()
        {
            string code = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            if (!CultureIsSupported(code))
                return DefaultCulture.TwoLetterISOLanguageName;

            string shortForm = ConvertToShortForm(code).ToLowerInvariant();
            return shortForm;
        }

        public static void SetCulture(string code)
        {
            CultureInfo cultureInfo = GetCulture(code);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }
        
    }
}
