using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Reflection;

namespace TelesBot.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescribeAttribute[] attributes =
            (DescribeAttribute[])fi.GetCustomAttributes(
            typeof(DescribeAttribute),
            false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}