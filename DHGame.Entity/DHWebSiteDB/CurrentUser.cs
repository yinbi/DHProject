using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Entity.DHWebSiteDB
{
    /// <summary>
    /// 代码当前在线的用户
    /// </summary>
    [Serializable]
    public class CurrentUser
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        ///// <summary>
        ///// 角色id集合
        ///// </summary>
        //public List<int> RIds { get; set; }

        


    }
}
