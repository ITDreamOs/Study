using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Redis.Service.DTO
{
    public class RedisGeoDbModel
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double lng { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int  GeoKey { get; set; }

    }
}
