using lvwei8.Redis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Redis
{

    /// <summary>
    /// redis服务
    /// </summary>
    public class RedisService
    {
        #region 单例模式
        private static RedisService RedisServiceInstance;
        private static readonly object locker = new object();
        private RedisService() { }
        public static RedisService GetService()
        {
            if (RedisServiceInstance == null)
            {
                lock (locker)
                {
                    return new RedisService();
                }
            }
            return RedisServiceInstance;
        }
        #endregion

        /// <summary>
        /// 设值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">键</param>
        /// <param name="t">值</param>
        /// <returns></returns>
        public  bool Set<T>(string key, T t)
        {
            return RedisManager.Set(key, t);
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return RedisManager.Get<T>(key);
        }



    }
}
