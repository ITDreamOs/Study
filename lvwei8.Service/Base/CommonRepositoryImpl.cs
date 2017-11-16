
using lvwei8.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base
{

    public class CommonRepositoryImpl : ICommonRepository
    {
        #region 属性区域

        /// <summary>
        /// db上下文
        /// </summary>
        public Lvwei8MySqlReadOnlyEntities DbContext { private get; set; }
        public Lvwei8MySqlEntities DbContextForTrans { private get; set; }
        public DbContext getDbContext()
        {
            return  DbContext;
        }
        #endregion

        #region 方法区域

        /// <summary>
        /// 获取服务器当前时间
        /// </summary>
        /// <returns>获取当前时间</returns>
        public DateTime GetServerNow()
        {
            return getDbContext().Database.SqlQuery<DateTime>("select current_timestamp()").First();
        }

        /// <summary>
        /// SQL语句查询
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>查询到的结果</returns>
        public virtual IQueryable<TEntity> SelectQuery<TEntity>(string query, params object[] parameters)
        {
            return getDbContext().Database.SqlQuery<TEntity>(query, parameters).AsQueryable();
        }


        /// <summary>
        /// SQL执行
        /// </summary>
        /// <param name="sqlstr">执行语句</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>执行行数</returns>
        public virtual int ExecuteSqlCmd(string sqlstr, params object[] parameters)
        {
            return DbContextForTrans.Database.ExecuteSqlCommand(sqlstr, parameters);
        }
        #endregion
    }
}
