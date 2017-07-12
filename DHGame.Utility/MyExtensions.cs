using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHGame.Utility
{
    public static class MyExtensions
    {
        /// <summary>
        /// 过滤不安全字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SqlSafe(this string str)
        {
            if (str == null)
                return "";
            string[] temp = { "'", "|", "&", ";", "$", "%", "@", '"'.ToString(), "\'", "\"", "<>", "()", "+", "\r", "\n", ",", "\\", "--" };
            foreach (string s in temp)
            {
                str = str.Replace(s, "");
            }
            return str;
        }
        /// <summary>
        /// 过滤不安全字符(保留'|"|,)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SqlSafe1(this string str)
        {
            if (str == null)
                return "";
            string[] temp = { "&", ";", "$", "%", "@", "<>", "()", "+", "\r", "\n", "\\", "--" };
            foreach (string s in temp)
            {
                str = str.Replace(s, "");
            }
            return str;
        }
    }
}
