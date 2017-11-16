using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Redis.Service.DTO
{
    /// <summary>
    /// redis配置库
    /// </summary>
    public class RedisConfigureDbModel
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public const string UserDbModel = "user";

        /// <summary>
        /// 用户距离
        /// </summary>
        public const string GEODbModel = "GEO";


    }


    /// <summary>
    /// 数据库索引配置
    /// </summary>
    public enum RedisDbIndex
    {
        GEO = 1
    }
}
