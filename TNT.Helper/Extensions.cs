﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNTHelper
{
    public static class Extensions
    {

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
    }
}