using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Redis.Service.DTO
{
    public class GeoRadius
    {
        /// <summary>
        /// The Radius size of this GeoRadius command.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// The center point to base the search.
        /// </summary>
        public GeoPosition GeoPosition { get; }

        /// <summary>
        /// The key to use.
        /// </summary>
        public RedisKey Key { get; }

        /// <summary>
        /// The unit to return distance measurments in.
        /// </summary>
        public GeoUnit Unit { get; }

        /// <summary>
        /// The possible options for the GeoRadius command
        /// </summary>
        public GeoRadiusOptions GeoRadiusOptions { get; }

        /// <summary>
        /// The maximum number of results to return.
        /// However note that internally the command needs to perform an effort proportional to the number of items matching the specified area, so to query very large areas with a very small COUNT option may be slow even if just a few results are returned.
        /// </summary>
        public int MaxReturnCount { get; }
        public bool IsUsingWithOptions
        {
            get
            {
                var myWithOptions = this.GeoRadiusOptions & (GeoRadiusOptions.WithCoordinates | GeoRadiusOptions.WithDistance | GeoRadiusOptions.WithGeoHash);
                return myWithOptions != GeoRadiusOptions.None;
            }
        }

        /// <summary>
        /// Creates a new GeoRadius
        /// </summary>
        public GeoRadius(RedisKey key, GeoPosition geoPosition, double radius, int maxReturnCount = -1, GeoUnit unit = GeoUnit.Meters, GeoRadiusOptions geoRadiusOptions = (GeoRadiusOptions.WithCoordinates | GeoRadiusOptions.WithDistance))
        {
            Key = key;
            GeoPosition = geoPosition;
            Radius = radius;
            Unit = unit;
            GeoRadiusOptions = geoRadiusOptions;
            MaxReturnCount = maxReturnCount;
        }

        public bool HasFlag(GeoRadiusOptions flag)
        {
            return (GeoRadiusOptions & flag) != 0;
        }

        /// <summary>
        /// Converts into an array of RedisValues which can be sent over the wire.
        /// </summary>
        /// <returns></returns>
        internal RedisValue[] ToRedisValueArray()
        {
            var redisValues = new System.Collections.Generic.List<RedisValue>();
            redisValues.Add(this.GeoPosition.Longitude);
            redisValues.Add(this.GeoPosition.Latitude);
            redisValues.Add(this.Radius);
            redisValues.Add(GeoConst._redisUnits[(int)this.Unit]);
            if (this.HasFlag(GeoRadiusOptions.WithCoordinates))
                redisValues.Add("WITHCOORD");
            if (this.HasFlag(GeoRadiusOptions.WithDistance))
                redisValues.Add("WITHDIST");
            if (this.HasFlag(GeoRadiusOptions.WithGeoHash))
                redisValues.Add("WITHHASH");
            if (this.MaxReturnCount >= 0)
            {
                redisValues.Add("COUNT");
                redisValues.Add(this.MaxReturnCount);
            }
            if (this.HasFlag(GeoRadiusOptions.Ascending))
                redisValues.Add("ASC");
            else if (this.HasFlag(GeoRadiusOptions.Descending))
                redisValues.Add("DESC");
            return redisValues.ToArray();
        }
    }

    /// <summary>
    /// Constants used in geo calculations.
    /// </summary>
    public static class GeoConst
    {
        /// <summary>
        /// The radius of the earth in meters.
        /// </summary>
        public const double RadiusOfEarthMeters = 6372797.560856; //From the redis 3.2 implementation.

        /// <summary>
        /// The number of radians in a degree.
        /// </summary>
        public const double RadiansPerDegree = 0.017453292519943295769236907684886; //From redis 3.2 implementation.

        internal static string[] _redisUnits = new[] { "m", "km", "mi", "ft" };
    }

    [Flags]
    public enum GeoRadiusOptions
    {
        /// <summary>
        /// No Options
        /// </summary>
        None = 0,
        /// <summary>
        /// Redis will return the coordinates of any results.
        /// </summary>
        WithCoordinates = 1,
        /// <summary>
        /// Redis will return the distance from center for all results.
        /// </summary>
        WithDistance = 2,
        /// <summary>
        /// Redis will return the geo hash value as an integer. (This is the score in the sorted set)
        /// </summary>
        WithGeoHash = 4,
        /// <summary>
        /// Redis will return the results in ordered by distance. The nearest items will be first.
        /// </summary>
        Ascending = 8,
        /// <summary>
        /// Redis will return the results in ordered by distance. The farthest items will be first.
        /// </summary>
        Descending = 16,
    }
}
