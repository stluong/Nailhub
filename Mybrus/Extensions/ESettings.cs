using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Mybrus.Extensions
{
    public class ESettings
    {
        public enum Role { 
            [DefaultValue("Admin")]
            Admin,
            [DefaultValue("Site Manager")]
            SiteManager
        }

        public static class Get {

            #region EnumHelp
            public static string Description(Enum myEnum)
            {
                FieldInfo fi = myEnum.GetType().GetField(myEnum.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return myEnum.ToString();
            }
            public static T Value<T>(Enum myEnum)
            {
                FieldInfo fi = myEnum.GetType().GetField(myEnum.ToString());
                DefaultValueAttribute[] attributes =
                    (DefaultValueAttribute[])fi.GetCustomAttributes(typeof(DefaultValueAttribute), false);

                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (attributes != null && attributes.Length > 0)
                    return (T)(converter.ConvertFrom(attributes[0].Value));
                else
                    return (T)(converter.ConvertFrom(myEnum));
            }
            public static string Value(Enum myEnum)
            {
                return Value<string>(myEnum);
            }
            public static string[] Values(params Enum[] myEnums)
            {
                List<string> arrValue = new List<string>();
                foreach (Enum myEnum in myEnums)
                {
                    var myValue = Value<object>(myEnum);
                    if (myValue.GetType() == typeof(string))
                    {
                        arrValue.Add(myValue.ToString());
                    }
                    else
                    {
                        arrValue.AddRange((string[])myValue);
                    }
                }
                return arrValue.ToArray();
            }

            #endregion
        }
    }

}