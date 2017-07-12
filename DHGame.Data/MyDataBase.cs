using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Data
{
    /// <summary>
    /// 数据库操作基类
    /// </summary>
    public class MyDataBase : DbContext
    {
        public MyDataBase(string connStr)
            : base(connStr)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();//防止黑幕交易 要不然每次都要访问 EdmMetadata这个表
            Database.SetInitializer<MyDataBase>(null);

            base.OnModelCreating(modelBuilder);
        }
    }
}
