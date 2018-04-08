using System.ComponentModel;
using System.Reflection;

namespace Esi_BusinessLayer.Common
{
    public static class EnumerationExtensions
    {
        public static string ToDescription<TEnum>(this TEnum value)
        {
            FieldInfo enumFields = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])enumFields.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
