using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnlineShop.Common.Utitlities
{
    public static class StringUtility
    {
        /// <summary>
        /// Generate slug string
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveSign4VietnameseString().ToLower();

            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();

            // cut and trim
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();

            str = Regex.Replace(str, @"\s", "-"); // hyphens
            return str;
        }

        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        /// <summary>
        /// Remove sign
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSign4VietnameseString(this string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        /// <summary>
        /// Get Random string
        /// </summary>
        private static Random random = new Random();

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static List<int> ConvertStringToListInt(string str, char seperator)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return str.Split(seperator).Select(int.Parse).ToList();
        }

        public static string TextOverEllipsis(this string str)
        {
            if (str.Length > 20)
            {
                str = str.Substring(0, 20);
                return $"{str}...";
            }
            return str;
        }
    }
}