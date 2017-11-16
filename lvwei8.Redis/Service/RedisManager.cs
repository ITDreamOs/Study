using lvwei8.Redis.Service.DTO;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace lvwei8.Redis.Service
{
    public class RedisManager
    {

        private static ConnectionMultiplexer redis = null;
        public static ConnectionMultiplexer getRedis()
        {
          
            if (redis == null)
                redis = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisTracker"].ToString());
            return redis;
        }

        private IDatabase GetDataBase(int index = -1)
        {
            return getRedis().GetDatabase(index);
        }

        #region string类型
        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>System.String.</returns>
        public string StringGet(string key)
        {
            try
            {
                using (var client = getRedis())
                {
                    return client.GetDatabase().StringGet(key);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 批量获取值
        /// </summary>
        public string[] StringGetMany(string[] keyStrs)
        {
            var count = keyStrs.Length;
            var keys = new RedisKey[count];
            var addrs = new string[count];

            for (var i = 0; i < count; i++)
            {
                keys[i] = keyStrs[i];
            }
            try
            {
                using (var client = getRedis())
                {
                    var values = client.GetDatabase().StringGet(keys);
                    for (var i = 0; i < values.Length; i++)
                    {
                        addrs[i] = values[i];
                    }
                    return addrs;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 单条存值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StringSet(string key, string value)
        {

            using (var client = getRedis())
            {
                return client.GetDatabase().StringSet(key, value);
            }
        }


        /// <summary>
        /// 批量存值
        /// </summary>
        /// <param name="keysStr">key</param>
        /// <param name="valuesStr">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StringSetMany(string[] keysStr, string[] valuesStr)
        {
            var count = keysStr.Length;
            var keyValuePair = new KeyValuePair<RedisKey, RedisValue>[count];
            for (int i = 0; i < count; i++)
            {
                keyValuePair[i] = new KeyValuePair<RedisKey, RedisValue>(keysStr[i], valuesStr[i]);
            }
            using (var client = getRedis())
            {
                return client.GetDatabase().StringSet(keyValuePair);
            }
        }

        #endregion

        #region 泛型
        /// <summary>
        /// 存值并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="t">实体</param>
        /// <param name="ts">过期时间间隔</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Set<T>(string key, T t, TimeSpan ts)
        {

            var str = JsonConvert.SerializeObject(t);

            using (var client = getRedis())
            {
                return client.GetDatabase().StringSet(key, str, ts);
            }
        }

        /// <summary>
        /// 存值并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="t">实体</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Set<T>(string key, T t)
        {
            var str = JsonConvert.SerializeObject(t);
            var client = getRedis();
            return client.GetDatabase().StringSet(key, str);
        }



        /// <summary>
        /// 
        /// 根据Key获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public static T Get<T>(string key)
        {

            var strValue = getRedis().GetDatabase().StringGet(key);
            if (string.IsNullOrEmpty(strValue))
            {
                throw new Exception("不存在");
            }
            return JsonConvert.DeserializeObject<T>(strValue);

        }
        #endregion

        #region geo
        /// <summary>
        /// 设置添加用户经纬度
        /// </summary>
        /// <param name="geoModel"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Set(RedisGeoDbModel geoModel, string key = RedisConfigureDbModel.GEODbModel)
        {
            var result = false;
            using (var client = getRedis())
            {
                var db = client.GetDatabase((int)RedisDbIndex.GEO);
                db.GeoAdd(key, geoModel.lng, geoModel.lat, geoModel.GeoKey);
            }
            return result;
        }


        /// <summary>
        /// 获取用户经纬度
        /// </summary>
        /// <param name="geoModel"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public RedisGeoDbModel Get(RedisGeoDbModel geoModel, string key = RedisConfigureDbModel.GEODbModel)
        {
            var result = new RedisGeoDbModel();
            using (var client = getRedis())
            {
                var db = client.GetDatabase((int)RedisDbIndex.GEO);
                var geoPoint = db.GeoPosition(key, geoModel.GeoKey);
                if (geoPoint == null)
                {
                    return null;
                }
                result.lng = geoPoint.Value.Longitude;
                result.lat = geoPoint.Value.Latitude;
                result.GeoKey = geoModel.GeoKey;
            }
            return result;
        }




        #region geo search 
        /// <summary>
        /// 附近的人,最多返回100个
        /// </summary>
        /// <param name="lat">维度</param>
        /// <param name="lng">经度</param>
        /// <param name="radius">半径,单位为米,当radius为零时表示使用默认初始范围，并自动扩大范围, 每次扩大20km,最大到500km</param>
        private IEnumerable<GeoRadiusResult> NearbyUsers(double lat, double lng, double radius, ref PageViewModel page,
            Func<int[], int[]> filterUser = null, string key = RedisConfigureDbModel.GEODbModel)
        {
            var needExt = radius == 0; //是否需要自动扩展
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var extStep = 100000;
            var extMax = 20000000;
            if (radius == 0) radius = extStep;
            page = page ?? PageViewModel.Default();

            var db = getRedis().GetDatabase((int)RedisDbIndex.GEO);
            var pos = new GeoPosition(lng, lat);
            GeoRadius geoRadius;
            List<GeoRadiusResult> finalResult = new List<GeoRadiusResult>();

            var extTimes = 0;
            var preCount = 0;//记录上次查询到的数量，, 再过滤时跳过
            while (extTimes == 0 || (needExt && !page.CurrentPageIsFull && radius < extMax))
            {
                geoRadius = new GeoRadius(
                    key,
                    pos, radius, 20000000,
                    GeoUnit.Meters,
                    DTO.GeoRadiusOptions.WithCoordinates | DTO.GeoRadiusOptions.WithDistance
                );
                var result = db.GeoRadius(key, geoRadius.GeoPosition.Longitude, geoRadius.GeoPosition.Latitude, radius);
                if (result.Count() > preCount) //有新id拿到才需要过滤
                {

                    var newResult = result.Skip(preCount).ToArray();
                    var newUserIds = newResult.Select(e => Int32.Parse(e.Member)).ToArray();
                    //找出新查询到的UserId,
                    if (filterUser != null)//Remove excluded 
                    {
                        newUserIds = filterUser(newUserIds).ToArray();
                    }
                    finalResult.AddRange(newResult.Where(e => newUserIds.Contains(Int32.Parse(e.Member))));
                }
                preCount = result.Count();
                extTimes++;
                radius += extStep;
                page.RecordCount = finalResult.Count();
            }
            //处理最后页一条正好满的情况. 再增加一页
            if (page.CurrentPageIsFull && page.PageNo == page.PageCount)
                page.RecordCount += 1;
            watch.Stop();
            return finalResult.Skip(page.Skip).Take(page.PageSize);
        }

        private double? UserDisc(int centerUserId, int userId2, string key = RedisConfigureDbModel.GEODbModel)
        {
            double? result = 0;
            using (var client = getRedis())
            {
                var db = client.GetDatabase((int)RedisDbIndex.GEO);
                result = db.GeoDistance(key, centerUserId, userId2);
            }

            return result;
        }
        private Dictionary<int, double?> UserDisc(int centerUserId, HashSet<int> userIds, string key = RedisConfigureDbModel.GEODbModel)
        {
            Stopwatch watch = Stopwatch.StartNew();
            watch.Stop();
            var result = new Dictionary<int, double?>();
            using (var client = getRedis())
            {
                var db = client.GetDatabase((int)RedisDbIndex.GEO);
                userIds.ToList().ForEach(e =>
                {
                    result.Add(e, db.GeoDistance(key, centerUserId, e));
                });
            }
            return result;
        }
        #endregion

        #endregion

        #region sortset
        /// <summary>
        /// 存值并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="t">实体</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SortSetAdd<T>(string key, T t)
        {
            var result = false;
            using (var client = getRedis())
            {
                var CreateTime = DateTime.Now;
                var dbResult = JsonConvert.SerializeObject(t);
                client.GetDatabase().SortedSetAdd(key, dbResult, CreateTime.ToOADate());
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public List<T> GetSortSet<T>(string key) where T : class
        {
            using (var client = getRedis())
            {
                var dbresult = client.GetDatabase().SortedSetRangeByRank(key);
                if (dbresult == null)
                {
                    return null;
                }
                if (dbresult.Length == 0)
                {
                    return null;
                }
                var result = dbresult.Cast<T>().ToList();
                return result;
            }
        }
        #endregion
    }
}
