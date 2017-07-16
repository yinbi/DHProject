using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DHGame.Utility.Extensions;

namespace DHGame.Utility
{
    public static class WebUtil
    {
        /// <summary>
        ///  返回表单参数值 或者 URL查询参数值 并将其转换提T类型 如果转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值 </param>
        /// <returns>转换成功则返回转换的值，否则返回defaultValue</returns>
        public static T Get<T>(string name, T defaultValue)
        {
            string str = GetOrNull(name);

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.SqlSafe().ToType<T>(defaultValue);
        }
        /// <summary>
        ///返回FORM表单参数值 或者 URL查询参数值 如果不存在则返回Null
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回 Null</returns>
        public static string GetOrNull(string name)
        {
            return GetFormOrNull(name) ?? GetQueryOrNull(name);
        }
        /// <summary>
        /// 返回FORM表单参数值 或者 URL查询参数值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回  string.Empty </returns>
        public static string Get(string name)
        {
            string str = GetFormOrNull(name) ?? GetQueryOrNull(name);
            return str ?? string.Empty;
        }
        /// <summary>
        /// 返回FORM表单参数值 如果不存在则返回Null
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回 Null</returns>
        public static string GetFormOrNull(string name)
        {
            return HttpContext.Current.Request.Form[name];
        }
        /// <summary>
        /// 返回URL查询参数值 如果不存在则返回Null
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回 Null</returns>
        public static string GetQueryOrNull(string name)
        {
            return HttpContext.Current.Request.QueryString[name];
        }
    }
}
