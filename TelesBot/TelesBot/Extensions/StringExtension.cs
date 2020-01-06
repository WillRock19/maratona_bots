using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Reflection;

namespace TelesBot.Extensions
{
    public static class StringExtension
    {
        public static T ToEnumByDescribe<T>(this string describe)
        {
            MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)
            {
                DescribeAttribute[] attributes = (DescribeAttribute[])fi.GetCustomAttributes(typeof(DescribeAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description.Contains(describe))
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            throw new Exception("Describe not found: " + describe);
        }

        public static bool HasValue(this string text) => 
            !string.IsNullOrWhiteSpace(text);
    }
}