using lvwei8.Model;
using lvwei8.MvcBackend.Codes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace lvwei8.MvcBackend.Common
{
    public class AreaUtil
    {

        /// <summary>
        /// 省直辖截取映射
        /// </summary>
        public static string[] MunicipalityAreaCodePrefix = new string[] { "11", "12", "31", "50" };


        /// <summary>
        /// 省直辖县区域码
        /// </summary>
        public static List<string> ProvinceMunicipalityAreaCode = new List<string>() { "4190", "4290", "4690" };


        public static Dictionary<string, string> getAllAreas()
        {
            ObjectCache cache = MemoryCache.Default;
            Dictionary<string, string> allAreas = cache["AllAreas"] as Dictionary<string, string>;
            if (allAreas == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                string monitorFilePath = HttpContext.Current.Server.MapPath("~/CacheFileDependency/AreaList.cache");
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<String> { monitorFilePath }));

                var dbContext = new Lvwei8MySqlReadOnlyEntities();
                allAreas = dbContext.Areas.OrderBy(e => e.Code).ToList().ToDictionary(i => i.Code, i => i.Name);
                cache.Set("AllAreas", allAreas, policy);
            }
            return allAreas;
        }
        /// <summary>
        /// 获取区域名称，包括上级行政区
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static String getAreaByCode(string codes)
        {
            if (String.IsNullOrEmpty(codes) || codes == "0")
            {
                return null;
            }
            List<string> names = new List<string>();
            var areas = AreaUtil.getAllAreas();
            string lastParentLevel = null;
            foreach (var i in codes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).OrderBy(e => e))
            {
                if (!areas.ContainsKey(i))
                {
                    log4net.LogManager.GetLogger(GlobalVars.MainLog_LoggerName).Error("没有找到相应的区域编码：" + i);
                    continue;
                }
                var name = areas[i];
                var level = getAreaLevel(i);
                //如果是市级, 则查询省名称
                if (level == 2)
                {
                    var parentArea = getAllAreas()[i.Substring(0, 2) + "0000"];
                    if (parentArea != lastParentLevel)
                        name = parentArea + " " + name;
                    lastParentLevel = parentArea;
                }
                else if (level == 3)
                {

                    var parentArea = "";
                    if (ProvinceMunicipalityAreaCode.Contains(i.Substring(0, 4)) || MunicipalityAreaCodePrefix.Contains(i.Substring(0, 2)))
                    {
                        parentArea = getAllAreas()[i.Substring(0, 2) + "0000"];
                    }
                    else
                    {
                        parentArea = getAllAreas()[i.Substring(0, 4) + "00"];
                    }


                    if (parentArea != lastParentLevel)
                        name = parentArea + " " + name;
                    lastParentLevel = parentArea;
                }

                names.Add(name);
            }
            return String.Join(", ", names);
        }

        private static int getAreaLevel(string areaCodeStr)
        {
            //string areaCodeStr = areaCode.ToString();
            if (areaCodeStr.EndsWith("0000"))
            {
                return 1;
            }
            else if (areaCodeStr.Substring(2, 2) != "00" && areaCodeStr.Substring(4, 2) == "00")
            {
                return 2;
            }
            return 3;
        }

        public static List<KeyValuePair<string, string>> getAllProvinces()
        {
            return getAllAreas().Where(e => e.Key.ToString().EndsWith("0000")).ToList();
        }
        /// <summary>
        /// 返回当前区域的省
        /// </summary>
        /// <param name="areas"></param>
        /// <returns></returns>
        public static KeyValuePair<string, string>? getProvinByAreas(string area)
        {
            if (String.IsNullOrEmpty(area))
                return null;
            string firstCity = area;//areas.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
            string provinceCode = firstCity.Substring(0, 2) + "0000";
            return getAllProvinces().FirstOrDefault(e => e.Key == provinceCode);
        }

        

        internal static bool TryParse(string searchKey, out string[] areaCode)
        {
            bool matched = false;
            areaCode = null;
            var names = searchKey.Split(new[] { ' ', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //先找最后一个名字
            var areaMatched = getAllAreas().Where(e => e.Value == names[names.Length - 1]).ToList();
            //如果只有一个则直接确定
            if (areaMatched.Count == 1)
            {
                areaCode = new string[] { areaMatched[0].Key.ToString() };
                matched = true;
            }
            else if (areaMatched.Count > 1 && names.Length > 1) //找到多个，则一定是区县，则找上一级,上一级不应该有重名
            {
                var area = getAllAreas().Where(e => e.Value == names[names.Length - 2]).ToList();
                if (area.Count > 0)
                {
                    var distinctedAreaCode = areaMatched.Where(e => e.Key.ToString().Substring(0, 4) == area[0].Key.ToString().Substring(0, 4)).Select(e => e.Key.ToString()).FirstOrDefault();
                    if (!String.IsNullOrEmpty(distinctedAreaCode))
                    {
                        areaCode = new string[] { distinctedAreaCode };
                        matched = true;
                    }
                    //从areaMatched中找到匹配的, 如果没找到匹配的（类似：武汉市 金水区， 河南省 金水区），则返回空；
                }
            }
            else if (areaMatched.Count > 1 && names.Length == 1) //找到多个，并且只有一个名字的，例如：金水区，则返回多个AreaCode, 这有助于分站的查询的情况。
            {
                areaCode = areaMatched.Select(e => e.Key.ToString()).ToArray();
                matched = true;
            }
            return matched;
        }
    }
}