using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base
{
    public interface ICommonRepository
    {
        /// <summary>
        /// 获取服务器当前时间
        /// </summary>
        /// <returns>获取当前时间</returns>
        DateTime GetServerNow();

        /// <summary>
        /// SQL语句查询
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>查询到的结果</returns>
        IQueryable<TEntity> SelectQuery<TEntity>(string query, params object[] parameters);


        /// <summary>
        /// SQL执行
        /// </summary>
        /// <param name="sqlstr">执行语句</param>
        /// <param name="parameters">执行参数</param>
        /// <returns>执行行数</returns>
        int ExecuteSqlCmd(string sqlstr, params object[] parameters);
    }
}
