using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DHGame.Utility
{
    public class MyJson
    {
        /// <summary>
        /// 序列化操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            var ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            var retVal = Encoding.UTF8.GetString(ms.ToArray());
            ms.Dispose();
            return retVal;
        }

        /// <summary>
        /// 反序列化操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="json">字符传</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();
            return obj;
        }

        ///<summary>
        /// 返回easyui中datagrid使用的json格式
        ///</summary>
        ///<param name="dt">datatable数据</param>
        ///<param name="count">总的条数</param>
        ///<returns></returns>
        public static string DataToJson(DataTable dt, int count)
        {
            var sbjson = new StringBuilder();
            sbjson.Append("{");
            sbjson.Append("\"total\":" + count + ",\"rows\":[");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        sbjson.Append(",");
                        sbjson.Append("{");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dt.Columns.IndexOf(dc) > 0)
                            {
                                sbjson.Append(",");
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                            else
                            {
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                        }
                        sbjson.Append("}");
                    }
                    else
                    {
                        sbjson.Append("{");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dt.Columns.IndexOf(dc) > 0)
                            {
                                sbjson.Append(",");
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                            else
                            {
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                        }
                        sbjson.Append("}");
                    }
                }
            }
            sbjson.Append("]}");
            return sbjson.ToString();
        }

        /// <summary>
        /// 将datatable转换成json
        /// </summary>
        /// <param name="dt"> adminTree 返回的datatable</param>
        /// <returns></returns>
        public static string TreeDataToJson(DataTable dt)
        {
            var sbjson = new StringBuilder();
            sbjson.Append("[");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbjson.Append("{");
                    var pid = MyParse.ParseInt(dt.Rows[i]["ParentID"].ToString(), -1);
                    if (pid == 0)
                    {
                        //{ id: 2, pId: 0, name: "推广查询", open: true },
                        sbjson.Append("\"id\":\"" + dt.Rows[i]["ID"].ToString().Trim() + "\",\"pId\":\"0\",\"name\":\"" + dt.Rows[i]["TreeName"].ToString().Trim() + "\",\"open\":\"true\",\"isParent\":\"true\"");
                    }
                    else if (pid > 0)
                    {
                        // { id: 11, pId: 1, name: "首页", t: "/Notices/IndexCenter.aspx" },
                        sbjson.Append("\"id\":\"" + dt.Rows[i]["ID"].ToString().Trim() + "\",\"pId\":\"" + dt.Rows[i]["ParentID"].ToString().Trim() + "\",\"name\":\"" + dt.Rows[i]["TreeName"].ToString().Trim() + "\",\"t\":\"" + dt.Rows[i]["HttpUrl"].ToString().Trim() + "\",\"isParent\":\"false\"");
                    }
                    sbjson.Append("}");
                    if (i < dt.Rows.Count - 1)
                        sbjson.Append(",");
                }
            }
            sbjson.Append("]");
            return sbjson.ToString();
        }

        /// <summary>
        /// 将datatable转换成json
        /// </summary>
        /// <param name="dt"> adminTree 返回的datatable</param>
        /// <returns></returns>
        public static string CheckTreeDataToJson(DataTable dt)
        {
            var sbjson = new StringBuilder();
            sbjson.Append("[");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbjson.Append("{");
                    var pid = MyParse.ParseInt(dt.Rows[i]["ParentID"].ToString(), -1);
                    if (pid == 0)
                    {
                        //{ id: 2, pId: 0, name: "推广查询", open: true },
                        sbjson.Append("\"id\":\"" + dt.Rows[i]["ID"].ToString().Trim() + "\",\"pId\":\"0\",\"name\":\"" + dt.Rows[i]["TreeName"].ToString().Trim() + "\",\"open\":\"true\",\"isParent\":\"true\",\"checked\":\"" + dt.Rows[i]["CheckStr"] + "\"");
                    }
                    else if (pid > 0)
                    {
                        // { id: 11, pId: 1, name: "首页", t: "/Notices/IndexCenter.aspx" },
                        sbjson.Append("\"id\":\"" + dt.Rows[i]["ID"].ToString().Trim() + "\",\"pId\":\"" + dt.Rows[i]["ParentID"].ToString().Trim() + "\",\"name\":\"" + dt.Rows[i]["TreeName"].ToString().Trim() + "\",\"t\":\"" + dt.Rows[i]["HttpUrl"].ToString().Trim() + "\",\"isParent\":\"false\",\"checked\":\"" + dt.Rows[i]["CheckStr"] + "\"");
                    }
                    sbjson.Append("}");
                    if (i < dt.Rows.Count - 1)
                        sbjson.Append(",");
                }
            }
            sbjson.Append("]");
            return sbjson.ToString();
        }

        /// <summary>
        /// 组装json
        /// </summary>
        /// <param name="isSuccess">状态</param>
        /// <param name="message">信息</param>
        /// <returns></returns>
        public static string MessageToJson(int isSuccess, string message)
        {
            var sbjson = new StringBuilder();
            sbjson.Append("{");
            sbjson.Append("\"IsSuccess\":\"" + isSuccess + "\",\"Message\":\"" + message + "\"");
            sbjson.Append("}");
            return sbjson.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="message">返回的信息</param>
        /// <returns></returns>
        public static string MessageToJson(string isSuccess, string message, string result)
        {
            var sbjson = new StringBuilder();
            sbjson.Append("{");
            sbjson.Append("\"IsSuccess\":\"" + isSuccess + "\",\"Message\":\"" + message + "\",\"result\":" + message + "");
            sbjson.Append("}");
            return sbjson.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string MessageToJson(int isSuccess, string message, string data)
        {
            var sbjson = new StringBuilder();
            sbjson.Append("{");
            if (!string.IsNullOrEmpty(data))
            {
                sbjson.Append("\"IsSuccess\":\"" + isSuccess + "\",\"Message\":\"" + message + "\",\"Data\":" + data + "");
            }
            else
            {
                sbjson.Append("\"IsSuccess\":\"" + isSuccess + "\",\"Message\":\"" + message + "\"");
            }
            sbjson.Append("}");
            return sbjson.ToString();
        }

        //DataTable转成Json
        public static string DataTableToJson(DataTable dt)
        {
            var json = new StringBuilder();
            json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        json.Append("\"" + dt.Columns[j].ColumnName.ToString(CultureInfo.InvariantCulture) + "\":\"" + dt.Rows[i][j] + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]");
            return json.ToString();
        }

        /// <summary>
        /// List转成json
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jsonName">返回json的名字</param>
        /// <param name="il">泛型集合</param>
        /// <returns></returns>
        public static string ObjectToJson<T>(string jsonName, IList<T> il)
        {
            var json = new StringBuilder();
            json.Append("{\"" + jsonName + "\":[");
            if (il.Count > 0)
            {
                for (int i = 0; i < il.Count; i++)
                {
                    var obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        json.Append("\"" + pis[j].Name.ToString(CultureInfo.InvariantCulture) + "\":\"" + pis[j].GetValue(il[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (i < il.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]}");
            return json.ToString();
        }

        /// <summary>
        /// List转成json
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="il">泛型集合</param>
        /// <returns></returns>
        public static string ObjectToJson<T>(IList<T> il)
        {
            var json = new StringBuilder();
            json.Append("{[");
            if (il.Count > 0)
            {
                for (int i = 0; i < il.Count; i++)
                {
                    var obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        json.Append("\"" + pis[j].Name.ToString(CultureInfo.InvariantCulture) + "\":\"" + pis[j].GetValue(il[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (i < il.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]}");
            return json.ToString();
        }

        /// <summary>
        /// 将获取的Json数据转换为DataTable
        /// </summary>
        /// <param name="strJson">Json字符串</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //取出表名  
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名  
            //strJson = strJson.Substring(strJson.IndexOf("[", System.StringComparison.Ordinal) + 1);
            //strJson = strJson.Substring(0, strJson.IndexOf("]", System.StringComparison.Ordinal));

            //获取数据  
            rg = new Regex(@"(?<={)[^}]+(?=})");
            var mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');

                //创建表  
                if (tb == null)
                {
                    tb = new DataTable { TableName = strName };
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容  
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }


    }
}
