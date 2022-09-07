using System.ComponentModel;
using System.Reflection;

namespace Framework.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;

            return source.ToString();
        }
    }
}
