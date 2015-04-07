using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mybrus.Extensions
{
    public static class ExBundle
    {
        public static IHtmlString Render(string path, object htmlAttributes)
        {
            return Render(path, new RouteValueDictionary(htmlAttributes));
        }

        public static IHtmlString Render(string path, IDictionary<string, object> htmlAttributes)
        {
            var attributes = BuildHtmlStringFrom(htmlAttributes);

            #if DEBUG
            var originalHtml = Styles.Render(path).ToHtmlString();
            string tagsWithAttributes = originalHtml.Replace("/>", attributes + "/>");
            return MvcHtmlString.Create(tagsWithAttributes);
            #endif

            string tagWithAttribute = string.Format(
                "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\"{1} />",
                Styles.Url(path), attributes);

            return MvcHtmlString.Create(tagWithAttribute);
        }

        private static string BuildHtmlStringFrom(IEnumerable<KeyValuePair<string, object>> htmlAttributes)
        {
            var builder = new StringBuilder();

            foreach (var attribute in htmlAttributes)
            {
                builder.AppendFormat(" {0}=\"{1}\"", attribute.Key, attribute.Value);
            }

            return builder.ToString();
        }
    }
}