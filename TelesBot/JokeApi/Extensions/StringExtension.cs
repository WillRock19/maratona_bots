using System;
using System.ComponentModel;
using System.Reflection;

namespace JokeApi.Extensions
{
    public static class StringExtension
    {
        public static T ToEnumThatHasThisDescription<T>(this string description)
        {
            MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description.Contains(description))
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            throw new Exception("Not found");
        }
    }
}
