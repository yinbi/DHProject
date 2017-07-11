using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DHGame.Enum
{
    public enum AuthActions
    {
        [Description("查询")]
        View = 1,
        [Description("增加")]
        Add = 2,
        [Description("修改")]
        Edit = 4,
        [Description("删除")]
        Delete = 8,
        [Description("下载")]
        Download = 16
    }
    #region
    //[Description("增加")]
    //Add = 1,
    //[Description("删除")]
    //Delete = 2,
    //[Description("修改")]
    //Edit = 4,
    //[Description("查询")]
    //View = 8,
    //[Description("输入")]
    //Import = 16,
    //[Description("输出")]
    //Output = 32,
    //[Description("下载")]
    //Download = 64,
    //[Description("打印")]
    //Print = 128
    #endregion
}