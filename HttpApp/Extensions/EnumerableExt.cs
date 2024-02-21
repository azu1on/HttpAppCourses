using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Extensions
{
    public static class EnumerableExt
    {
        public static string GetHtml<T>(this IEnumerable<T> courses)
        {
            Type type = typeof(T);
            var props = type.GetProperties();
            StringBuilder sb = new StringBuilder(100);
            sb.Append("</ul>");
            foreach (var course in courses)
            {
                foreach (var prop in type.GetProperties())
                {
                    sb.Append($"<li><span>{prop.Name}: </span>{prop.GetValue(course)}</li>");
                }
                sb.Append("</br>");
            }
            return sb.ToString();
        }
        public static string GetHtml<T>(this T course)
        {
            Type type = typeof(T);
            var props = type.GetProperties();
            StringBuilder sb = new StringBuilder(100);
            sb.Append("<ul>");
            foreach (var prop in props)
            {
                sb.Append($"<li><span>{prop.Name}: </span>{prop.GetValue(course)}</li>");
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}
