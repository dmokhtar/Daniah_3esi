using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
