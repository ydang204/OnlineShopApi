using System.Text.RegularExpressions;

namespace OnlineShop.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToRoute(this string input)
        {
            string[] split = Regex.Split(input, @"(?<!^)(?=[A-Z])");
            return string.Join("-", split);
        }
    }
}