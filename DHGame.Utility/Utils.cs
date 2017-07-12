using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DHGame.Utility
{
    public class Utils
    {
        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null) return false;
            var myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        ///// <summary>
        ///// 返回危险的那个字符
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static string ReturnDangerous(string key)
        //{
        //    string shield = ShieldKey;
        //    string[] keys = shield.Split(',');
        //    foreach (string i in keys)
        //    {
        //        if (key.Contains(i))
        //        {
        //            return i;
        //        }
        //    }
        //    return "";
        //}
        
    }
}
