using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace TNTHelper
{
    public static class Extensions
    {
        /// <summary>
        /// Convert connection string to dictionary EF connections with key for code first(CF) and database first (DF) connection string
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToEFConnection(this string nameOrConnectionString)
        {
            var myMetaData = "res://*";
            Dictionary<string, string> dicEFConnection = new Dictionary<string, string>();
            if (nameOrConnectionString.Like("name="))
            {
                nameOrConnectionString = AppSettings.GetConString(nameOrConnectionString.Replace("name=", ""));
                if (!nameOrConnectionString.Like(".csdl|res:", ".ssdl|res:"))
                {
                    try
                    {
                        nameOrConnectionString = Cryptography.DecryptString(nameOrConnectionString);
                    }
                    catch
                    {
                        //do nothing
                    }
                    finally
                    {
                        if (nameOrConnectionString.Like("=>"))
                        {
                            var tmp = nameOrConnectionString.Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToArray();
                            myMetaData = string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", tmp[0]);
                            nameOrConnectionString = tmp[1].Trim();
                        }
                    }
                }
            }
            var efConnection = new EntityConnectionStringBuilder
            {
                Metadata = myMetaData,
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = new SqlConnectionStringBuilder(nameOrConnectionString).ConnectionString
            };

            dicEFConnection.Add("CF", efConnection.ProviderConnectionString);
            dicEFConnection.Add("DF", efConnection.ConnectionString);

            return dicEFConnection;
        }
        /// <summary>
        /// Get connection string from nameOrConnectionString
        /// </summary>
        /// <param name="nameOrConnectionString">Could be encrypted nameOrConnectionString</param>
        /// <returns></returns>
        public static string GetConnnectionString(this string nameOrConnectionString)
        {
            if (nameOrConnectionString.Like("name="))
            {
                nameOrConnectionString = AppSettings.GetConString(nameOrConnectionString.Replace("name=", ""));
                if (!nameOrConnectionString.Like(".csdl|res:", ".ssdl|res:"))
                {
                    try
                    {
                        nameOrConnectionString = Cryptography.DecryptString(nameOrConnectionString);
                    }
                    catch
                    {
                        //do nothing
                    }
                }
            }

            return nameOrConnectionString;
        }
        public static bool IsIn<T>(T o, params T[] values)
        {
            foreach (T value in values)
            {
                if (value.Equals(o)) return true;
            }
            return false;
        }
        public static bool In(this int value, params int[] arg)
        {
            return IsIn<int>(value, arg);
        }
        public static bool In(this short value, params short[] arg)
        {
            return IsIn<short>(value, arg);
        }
        public static bool In(this char value, params char[] arg)
        {
            return IsIn<char>(value, arg);
        }
        public static bool In(this byte value, params byte[] arg)
        {
            return IsIn<byte>(value, arg);
        }
        public static bool In(this decimal value, params decimal[] arg)
        {
            return IsIn<decimal>(value, arg);
        }
        public static string CleanKoJS(this string value)
        {
            if (value.Like("__ko_mapping__", "error"))
            {
                value = value.Split(new string[] { ",\"__ko_mapping__", ",\"error" }, StringSplitOptions.None)[0];
                value = value + "}";
            }
            return value;
        }
        /// <summary>
        /// Empty, whitespace or "null" to null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EmptyToNull(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.ToLower().Trim() == "null")
            {
                return null;
            }
            return value;
        }
        public static string NullToEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return value;
        }
        public static T? ToNullable<T>(this String s) where T : struct
        {
            try
            {
                return (T?)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(s);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool Like(this string value, params string[] arg)
        {
            arg = arg
                .Where(a => !string.IsNullOrEmpty(a.Trim()))
                .ToArray<string>()
            ;

            foreach (var item in arg)
            {
                if (value.Contains(item, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
        public static bool IsNumeric(this string value)
        {
            decimal num;
            return decimal.TryParse(value, out num);
        }
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        public static bool IsEmpty(this string value)
        {
            return (value == null || value.Trim().Length == 0);
        }
        public static bool IsValidEmail(this string value)
        {
            value = value.Trim();
            Regex r = new Regex(AppSettings.Get<string>("EmailRegex") 
                ?? @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (r.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string ToMoney(this string value, string cultureInfo = "en-US") {
            return string.Format(CultureInfo.CreateSpecificCulture(cultureInfo),
                 "{0:C}", double.Parse(value))
            ;
        }

        public static string ToMoney(object value, string cultureInfo = "en-US") {
            return string.Format(CultureInfo.CreateSpecificCulture(cultureInfo),
                 "{0:C}", double.Parse(value.ToString()))
            ;
        }

        #region Join Extension
        public static IEnumerable<TResult> LeftJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      select result(s, left);

            return _result;
        }


        public static IEnumerable<TResult> RightJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from i in inner
                      join s in source
                      on fk(i) equals pk(s) into joinData
                      from right in joinData.DefaultIfEmpty()
                      select result(right, i);

            return _result;
        }


        public static IEnumerable<TResult> FullOuterJoinJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {

            var left = source.LeftJoin(inner, pk, fk, result).ToList();
            var right = source.RightJoin(inner, pk, fk, result).ToList();

            return left.Union(right);


        }


        public static IEnumerable<TResult> LeftExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      where left == null
                      select result(s, left);

            return _result;
        }

        public static IEnumerable<TResult> RightExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                 IEnumerable<TInner> inner,
                                                                                 Func<TSource, TKey> pk,
                                                                                 Func<TInner, TKey> fk,
                                                                                 Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from i in inner
                      join s in source
                      on fk(i) equals pk(s) into joinData
                      from right in joinData.DefaultIfEmpty()
                      where right == null
                      select result(right, i);

            return _result;
        }


        public static IEnumerable<TResult> FulltExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                IEnumerable<TInner> inner,
                                                                                Func<TSource, TKey> pk,
                                                                                Func<TInner, TKey> fk,
                                                                                Func<TSource, TInner, TResult> result)
        {
            var left = source.LeftExcludingJoin(inner, pk, fk, result).ToList();
            var right = source.RightExcludingJoin(inner, pk, fk, result).ToList();

            return left.Union(right);
        }
        #endregion

    }

}



