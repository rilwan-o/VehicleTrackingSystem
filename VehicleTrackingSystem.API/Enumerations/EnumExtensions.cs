using System;
using System.Reflection;

namespace VehicleTrackingSystem.API.Enumerations
{
    public static class EnumExtensions
    {
        public static string ResponseCode(this Enum value)
        {
            string result = value.ToString("D").PadLeft(2, '0');
            return result;
        }

        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            EnumDisplayAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(EnumDisplayAttribute))
            as EnumDisplayAttribute;

            return attribute == null ? value.ToString() : attribute.Name;
        }

        public static string Description(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            EnumDisplayAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(EnumDisplayAttribute))
            as EnumDisplayAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

    public class EnumDisplayAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
