
using lvwei8.Model.Models;
using lvwei8.Service.Area.DTO;
using lvwei8.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Area
{
    /// <summary>
    /// 区域
    /// </summary>
    public class AreaServiceImpl:IAreaService
    {

        #region 属性注入
        public IRepository<AreaDbModel> AreaRepository { get; set; }
        /// <summary>
        /// [ReadOnly]区域仓储
        /// </summary>
        public IReadOnlyRepository<AreaDbModel> ReadOnlyRepository { get; set; }
        /// <summary>
        /// 通用仓储
        /// </summary>
        public ICommonRepository CommonRepository { get; set; }
        #endregion

        #region 区域所需常量

        /// <summary>
        /// 直辖市代码
        /// </summary>
        public static string[] MunicipalityAreaCodes = new string[] { "110000", "120000", "310000", "500000" };
        /// <summary>
        /// 省直辖截取映射
        /// </summary>
        public static string[] MunicipalityAreaCodePrefix = new string[] { "11", "12", "31", "50" };

        /// <summary>
        /// 省直辖名称
        /// </summary>
        public static readonly string[] MunicipalitiesNames = new string[] { "北京市", "上海市", "天津市", "重庆市" };

        /// <summary>
        /// 市辖区，县等常用的名称
        /// </summary>
        public static readonly string[] MunicipaldistrictAndCountyNames = new string[] { "市辖区", "县" };

        /// <summary>
        /// 省直辖县区域码
        /// </summary>
        public static List<string> ProvinceMunicipalityAreaCode = new List<string>() { "4190", "4290", "4690" };

        /// <summary>
        /// 省级直辖区域名称
        /// </summary>
        public static readonly string[] ProvinceMunicipalityAreaNames = new string[] { "省直辖县级行政区划", "自治区直辖县级行政区划" };

        /// <summary>
        /// 默认区域码
        /// </summary>
        public const string DEFAULT_AREA_CODE = "0";

        /// <summary>
        /// 默认城市中心点
        ///上海市普陀区中山北路1715号浦发广场
        ///121.44148605938,31.259593833117
        /// </summary>
        public const string DEFAULT_CITY_CENTER_PIONT = "121.44148605938,31.259593833117";
        #endregion

        #region 辅助
        /// <summary>
        /// DB转视图
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private AreaViewModel ConvertDBToAreaView(AreaDbModel model)
        {

            var area = new AreaViewModel()
            {
                Code = model.Code,
                FirstLetter = model.FirstLetter,
                IsCityBlock = model.IsCityBlock,
                Name = model.Name,
                BaiduFullName = model.BaiduFullName,
                BaiduName = model.BaiduName,
                FullName = model.FullName,
                CityLevel = model.CityLevel,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
             
            };
            return area;
        }

        /// <summary>
        /// 数据库数据集合转视图字典
        /// </summary>
        /// <param name="dbModel"></param>
        /// <returns></returns>
        private Dictionary<string, AreaViewModel> converDbToView(List<AreaDbModel> dbModel)
        {
            Dictionary<string, AreaViewModel> result = new Dictionary<string, AreaViewModel>();
            foreach (var area in dbModel)
            {
                result.Add(area.Code, ConvertDBToAreaView(area));

            }
            return result;
        }
        #endregion

        #region 区域辅助工具

        #region 区域码截取处理(省级市、省直辖区划)

        /// <summary>
        /// 区域码截取用于检索
        /// </summary>
        /// <param name="areaCode">区域码</param>
        /// <returns></returns>
        private string AreaCodeSub(string areaCode)
        {
            if (string.IsNullOrEmpty(areaCode)) return string.Empty;
            if (MunicipalityAreaCodePrefix.Contains(areaCode.Substring(0, 2))) return areaCode.Substring(0, 2);
            return areaCode.Substring(0, 4);
        }

        /// <summary>
        /// 区域名称截取用于检索
        /// </summary>
        /// <param name="areaName">河南省,郑州市,金水区</param>
        /// <returns>郑州市</returns>
        private string AreaNameSub(string areaName)
        {
            if (!areaName.Contains(",")) return null;
            //区域名称数组
            var listareanames = areaName.Split(',').ToList();
            //查找关键字
            var IsContains = listareanames.Where(e => ProvinceMunicipalityAreaNames.Contains(e)).FirstOrDefault();
            if (IsContains != null)
            {
                //省级区划改为某个市
                listareanames[listareanames.Count - 2] = listareanames[listareanames.Count - 1];
            }
            return string.Join(",", listareanames);
        }

        #endregion

        #endregion

        #region 规则说明
        /*
         *1.（区域资源）可以根据城市等级划分，省：1 市：2 区:3 (市辖区：北京、上海等城市等级归为1级，省直辖区划区域：归为2级)
         *2.（上下级的行政区锁定）查询属于渐进型 3=>2=>1 或者1=>2=>3
         *3(定位).BaiduName与BaiduFullName用于百度定位对接 BaiDuFullName=>Code
         *4.Code与FullName的转换 Code=>FullName、FullName=>Code 目前前台只传输到二级的名称，三级的可用于后台管理，扩展前台应用
         */

        #endregion

        #region 区域资源


        #region 查询所有区域
        /// <summary>
        /// 查询所有区域
        /// </summary>
        /// <returns></returns>
        //[Cache.CacheMethod]
        static Dictionary<string, AreaViewModel> _singleArea;
        static object lockAreaCach = new object();
        private Dictionary<string, AreaViewModel> QueryArea()
        {
            if (_singleArea == null)
            {
                lock (lockAreaCach)
                {
                    if (_singleArea == null)
                    {
                        var dbModel = ReadOnlyRepository.Query().ToList();
                        _singleArea = converDbToView(dbModel);
                    }
                }
            }
            return _singleArea;
        }
        #endregion

        #region 获取所有的省(包括省级市)
        /// <summary>
        /// 获取所有的省 包括省级市
        /// </summary>
        /// <returns></returns>
        public List<AreaViewModel> GetAllProvinces()
        {
      
             var listareas = new List<AreaViewModel>();
            QueryArea().Where(e => e.Value.CityLevel == "1").ToList().ForEach(e => listareas.Add(e.Value));
            return listareas;
        }
        #endregion

        #region 获取所有的市(包括直辖县)
        /// <summary>
        /// 获取所有2级市(直辖市增为市级 例：北京市)
        /// </summary>
        /// <returns></returns>
        public List<AreaViewModel> GetAllCitys()
        {
            var listareas = new List<AreaViewModel>();
            QueryArea().Where(e => e.Value.CityLevel == "2" || MunicipalityAreaCodes.Contains(e.Key)).ToList().ForEach(e => listareas.Add(e.Value));
            return listareas;
        }
        #endregion

        #region 获取所有的区
        /// <summary>
        /// 获取所有3级的城市
        /// </summary>
        /// <returns></returns>
        public List<AreaViewModel> GetAllCountys()
        {
            var listareas = new List<AreaViewModel>();
            QueryArea().Where(e => e.Value.CityLevel == "3").ToList().ForEach(e => listareas.Add(e.Value));
            return listareas;
        }

        #endregion

        #endregion



        #region 下级行政区锁定

        #region 通过省级区域码获取行政管辖的（二级）城市
        /// <summary>
        /// 根据省区域码获取城市
        /// </summary>
        /// <param name="provinceCode">省区域码</param>
        /// <returns>响应</returns>
        public List<AreaViewModel> GetCitysByProvinceCode(string provinceCode)
        {
            var results = new List<AreaViewModel>();
            if (string.IsNullOrEmpty(provinceCode)) return results;
            return this.GetAllCitys().Where(e => e.Code.StartsWith(provinceCode.Substring(0, 2))).ToList();
        }
        #endregion

        #region 通过市级区域码获取行政管辖的（三级区域）
        /// <summary>
        /// 根据城市区域码获取所管辖的区域
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public List<AreaViewModel> GetCountyByCityCode(string cityCode)
        {
            var results = new List<AreaViewModel>();
            if (string.IsNullOrEmpty(cityCode)) return results;

            var lists = GetAllCountys().Where(e => e.Code.StartsWith(AreaCodeSub(cityCode))).ToList();
            lists = GetAllCountys().ToList().OrderByDescending(e => e.Code).ToList();
            return GetAllCountys().Where(e => e.Code.StartsWith(AreaCodeSub(cityCode))).ToList();
        }
        #endregion

        #endregion

        #region 上级区域行政锁定

        #region 根据区域码获取城市模型
        /// <summary>
        /// 根据区域码获取城市模型
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public AreaViewModel GetCityModelFromAreaCode(string areacode)
        {
            if (string.IsNullOrEmpty(areacode)) return null;
            var result = this.GetAllCitys().Where(e => e.Code.StartsWith(AreaCodeSub(areacode))).FirstOrDefault();
            return result;
        }
        #endregion

        #region 根据区域码获取省级模型
        /// <summary>
        /// 根据区域码获取省级模型
        /// </summary>
        /// <param name="citycode"></param>
        /// <returns></returns>
        public AreaViewModel GetProvinceModelFromCode(string citycode)
        {
            if (string.IsNullOrEmpty(citycode)) return null;
            var result = this.GetAllProvinces().Where(e => e.Code.StartsWith(citycode.Substring(0, 2))).FirstOrDefault();
            return result;
        }
        #endregion

        #region 根据区域（二级或者三级）全名称获取省级模型
        /// <summary>
        /// 根据区域全称获取其省级
        /// </summary>
        /// <param name="areaFullName">河南省;河南省,郑州市;河南省,郑州市,金水区</param>
        /// <returns>河南省</returns>
        public AreaViewModel GetProvinceModelFromAreaFullName(string areaFullName)
        {
            if (string.IsNullOrEmpty(areaFullName)) return null;
            //带,号的处理 
            if (areaFullName.Contains(",")) return this.GetAllProvinces().Where(e => e.Name == (AreaNameSub(areaFullName).Split(',')[0]) || areaFullName.Contains(e.FullName)).FirstOrDefault();
            //全称检索中含有的名称
            return this.GetAllProvinces().Where(e => areaFullName.Contains(e.Name)).FirstOrDefault();
        }
        #endregion

        #region 根据（二级或者三级）区域全名称获取二级城市模型
        /// <summary>
        /// 根据三级区域全名称获取二级城市模型
        /// </summary>
        /// <param name="areaFullName">郑州市;北京;河南省,郑州市;河南省,郑州市,二七区</param>
        /// <returns></returns>
        public AreaViewModel GetCityModelFromAreaFullName(string areaFullName)
        {
            if (string.IsNullOrEmpty(areaFullName)) return null;
            if (areaFullName.Contains(",")) return this.GetAllCitys().Where(e => e.Name == (areaFullName.Split(',')[1]) || areaFullName.Contains(e.FullName)).FirstOrDefault();
            return this.GetAllCitys().Where(e => areaFullName.Contains(e.Name)).FirstOrDefault();
        }

        #endregion

        #endregion

        #region 区域码与区域全名称互转（前后台支持）

        #region CodeToName
        /// <summary>
        /// 区域码转为全名称
        /// </summary>
        /// <param name="codes">区域码:410100,410105</param>
        /// <returns>河南省,郑州市;河南省,郑州市,二七区</returns>
        public string GetName(string codes)
        {
            if (string.IsNullOrEmpty(codes)) return "";
            if (codes.Contains(",")) return GetName(codes.Split(','));
            if (QueryArea().ContainsKey(codes)) return QueryArea()[codes].FullName;
            return string.Empty;
        }

        /// <summary>
        /// 区域码数组转全名称
        /// </summary>
        /// <param name="codes">[410101，410105]</param>
        /// <returns>河南省,郑州市,二七区;河南省,郑州市,金水区</returns>
        public string GetName(string[] codes)
        {
            var listnames = new List<string>();

            foreach (var item in codes)
            {
                var name = QueryArea().ContainsKey(item) ? QueryArea()[item].FullName : "";
                listnames.Add(name);
            }
            return string.Join(";", listnames);
        }

        #endregion

        #region NameToCode

        #region 带兼容性处理Code、Name互转（备用）
        /*
           /// <summary>
        /// 带兼容性的处理NameToCode
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public string GetCode(string names)
        {
            if (string.IsNullOrEmpty(names)) return "";
            if (names.Contains(";")) return GetCodes(names.Split(';'));
            //匹配兼容性
            var AreaNameCompaticityName = AreaNameCompaticity(names);
            if (string.IsNullOrEmpty(AreaNameCompaticityName)) return "";
            return QueryArea().Where(e => e.Value.FullName == AreaNameCompaticityName).FirstOrDefault().Key;

        }
        /// <summary>
        /// 带兼容性的处理NameToCode
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public string GetCodes(string[] names)
        {
            var listcodes = new List<string>();
            foreach (var item in names)
            {
                //兼容处理
                var AreaNameCompaticityName = AreaNameCompaticity(item);
                AreaNameCompaticityName = !string.IsNullOrEmpty(AreaNameCompaticityName) ? QueryArea().Where(e => e.Value.FullName == AreaNameCompaticityName).FirstOrDefault().Key : "";
                listcodes.Add(AreaNameCompaticityName);
            }
            return string.Join(",", listcodes);
        }
         
         */


        #endregion

        /// <summary>
        /// 一个或者多个区域的转换
        /// </summary>
        /// <param name="names">河南省,郑州市，金水区；</param>
        /// <returns>401005</returns>

        public string GetCode(string names)
        {
            if (string.IsNullOrEmpty(names)) return "";
            if (names.Contains(";")) return GetCodes(names.Split(';'));
            return QueryArea().Where(e => e.Value.FullName == names).FirstOrDefault().Key;
        }

        /// <summary>
        /// 名称数组转code
        /// </summary>
        /// <param name="names">全称数组;["河南省,郑州市，二七区","河南省,郑州市,金水区"]</param>
        /// <returns>401001,401005</returns>
        public string GetCodes(string[] names)
        {
            var listcodes = new List<string>();
            foreach (var item in names)
            {
                var code = QueryArea().Where(e => e.Value.FullName == item).FirstOrDefault().Key;
                listcodes.Add(code);
            }
            return string.Join(",", listcodes);
        }
        #endregion

        #endregion

        #region 区域搜索系统

        /// <summary>
        /// 区域检索系统
        /// </summary>
        /// <param name="searchKey">关键字</param>
        /// <returns></returns>
        public List<AreaViewModel> SearchArea(string searchKey)
        {
            var results = new List<AreaViewModel>();
            if (string.IsNullOrEmpty(searchKey))
            {
                return results;
            }
            //去掉头尾的空格
            searchKey = searchKey.Trim();
            var names = searchKey.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            ////拿到所有的省
            //var Provinces = GetAllProvinces();
            ////所有的市
            //var Citys = GetAllCitys();
            ////所有的区
            //var Countys = GetAllCountys();
            var Query = QueryArea().Where(e => e.Value.Name == names[names.Length - 1]).ToList();
            var QueryCount = Query.Count;
            if (QueryCount == 0)
            {
                return results;
            }
            Query.ForEach(e => results.Add(e.Value));
            if (QueryCount == 1)
            {
                //results.Add(Query[0].Value);
                return results;
            }
            if (names.Length == 1)
            {
                return results;
            }
            results = results.Where(e => e.FullName.Contains(string.Join(",", names))).ToList();
            return results;
        }

        public List<AreaViewModel> SearchAreaForSubstation(string searchKey)
        {
            searchKey = string.IsNullOrEmpty(searchKey) ? string.Empty : searchKey.Trim();
            if (string.IsNullOrEmpty(searchKey))
                return new List<AreaViewModel>();
            return QueryArea().Where(e => !string.IsNullOrWhiteSpace(e.Value.FullName) && e.Value.FullName.Contains(searchKey)).ToList().Select(e => e.Value).ToList();
        }

        #endregion

        #region 转换兼容处理(备用)
        /// <summary>
        /// 区域名称兼容
        /// 北京市,市辖区=>北京市,北京市 北京市,市辖区,朝阳区=>北京市，北京市，朝阳区
        /// </summary>
        /// <returns></returns>
        private string AreaNameCompaticity(string fullname)
        {
            if (string.IsNullOrEmpty(fullname)) return "";
            if (!fullname.Contains(",")) return fullname;
            var listnames = fullname.Split(',');
            //含有北京市 并且含有市辖区或者县
            var iscontains = (listnames.Where(e => MunicipaldistrictAndCountyNames.Contains(e)).FirstOrDefault() != null) && (listnames.Where(e => MunicipalitiesNames.Contains(e)).FirstOrDefault() != null);
            //北京市,市辖区=>北京市,北京市 北京市,市辖区,朝阳区=>北京市，北京市，朝阳区
            if (iscontains) listnames[1] = listnames[0]; return string.Join(",", listnames);
        }
        #endregion

        #region 锁定区域查询（映射）

        #region CodeToView（一对一）
        /// <summary>
        /// 根据Code获取Area
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public AreaViewModel GetAreaViewByCode(string code)
        {
            if (QueryArea().ContainsKey(code))
                return QueryArea()[code];
            return null;
        }
        #endregion

        #region CodeToFullName(后台用)

        /// <summary>
        /// CodesToFullNames(调用GetNames)
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>

        public virtual string CodesToFullNames(string codes)
        {
            return this.GetName(codes);
        }
        #endregion

        #endregion

        #region 缓存

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ClearCache()
        {
            _singleArea = null;
        }

        #endregion

        #region 保险

        /// <summary>
        /// 转化为保险区域
        /// </summary>
        /// <param name="areaCode">区域码</param>
        /// <returns></returns>
        public string ConvertToInsuranceArea(string areaCode)
        {
            return string.Concat("3", areaCode);
        }

        #endregion

        #region 直辖市

        /// <summary>
        /// 是否直辖市
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public bool IsMunicipalit(string areaCode)
        {
            var municipalits = new List<string>() { "11", "12", "50", "31" };
            if (!string.IsNullOrWhiteSpace(areaCode))
            {
                return municipalits.Where(e => areaCode.StartsWith(e)).Count() > 0;
            }
            return false;
        }

        #endregion
    }
}
