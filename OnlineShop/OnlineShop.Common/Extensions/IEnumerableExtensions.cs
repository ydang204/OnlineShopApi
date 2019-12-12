using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Is collection not null and has at least 1 element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasElement<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            System.Reflection.MemberInfo[] memberInfo =
                        genericEnumType.GetMember(GenericEnum.ToString());

            if ((memberInfo != null && memberInfo.Length > 0))
            {
                dynamic _Attribs = memberInfo[0].GetCustomAttributes
                      (typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Length > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs[0]).Description;
                }
            }

            return GenericEnum.ToString();
        }
    }
}