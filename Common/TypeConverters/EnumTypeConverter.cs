using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Common.TypeConverters
{
    public class EnumTypeConverter : EnumConverter
    {
        private Type _enumType;
        public EnumTypeConverter(Type type) : base(type)
        {
            _enumType = type;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            foreach (FieldInfo fi in _enumType.GetFields())
            {
                DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute( fi, typeof(DescriptionAttribute));

                if ((dna != null) && ((string)value == dna.Description))
                    return Enum.Parse(_enumType, fi.Name);
            }
            return Enum.Parse(_enumType, (string)value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, value));
            DescriptionAttribute dna =
              (DescriptionAttribute)Attribute.GetCustomAttribute(
                fi, typeof(DescriptionAttribute));

            if (dna != null)
                return dna.Description;
            else return value.ToString();
        }
    }
}
