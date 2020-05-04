using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace StudChoice.DAL.Models
{
    public enum Course
    {
        [Description("First")]
        First = 1,
        [Description("Second")]
        Second = 2,
        [Description("Third")]
        Third = 3,
        [Description("Fourth")]
        Fourth = 4
    }

    

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field != null)
            {
                var stringAttribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (stringAttribute != null)
                {
                    return stringAttribute.Description;
                }

                return field.Name;
            }

            return null;
        }
    }
}
