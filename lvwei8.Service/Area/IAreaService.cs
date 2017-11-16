using lvwei8.Service.Area.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Area
{
    public interface IAreaService
    {

        #region 获取资源集合
        /// <summary>
        /// 获取所有的省(包括省级市)
        /// </summary>
        /// <returns></returns>
        List<AreaViewModel> GetAllProvinces();
        /// <summary>
        /// 获取所有的城市（二级）
        /// </summary>
        /// <returns></returns>
        List<AreaViewModel> GetAllCitys();
        /// <summary>
        /// 获取所有的区县（三级）
        /// </summary>
        /// <returns></returns>
        List<AreaViewModel> GetAllCountys();

        #endregion


        #region 下级行政区域锁定
        /// <summary>
        /// 查找该省下所有的市
        /// </summary>
        /// <param name="provincecode">省的区域码</param>
        /// <returns></returns>
        List<AreaViewModel> GetCitysByProvinceCode(string provincecode);

        /// <summary>
        /// 获取该市里所有的区
        /// </summary>
        /// <param name="code">市的区域码</param>
        /// <returns></returns>
        List<AreaViewModel> GetCountyByCityCode(string code);


        #endregion


        #region 上级区域行政锁定
        /// <summary>
        /// 通过区域码拿到上一级市的模型
        /// </summary>
        /// <param name="areacode">区域码</param>
        /// <returns></returns>
        AreaViewModel GetCityModelFromAreaCode(string areacode);

        /// <summary>
        /// 通过区域码（城市或者区域）拿到省的模型
        /// </summary>
        /// <param name="citycode"></param>
        /// <returns></returns>
        AreaViewModel GetProvinceModelFromCode(string citycode);


        /// <summary>
        /// 根据全名称获取城市模型
        /// </summary>
        /// <param name="areaFullName">全名称:河南省,郑州市</param>
        /// <returns>郑州市</returns>
        AreaViewModel GetCityModelFromAreaFullName(string areaFullName);

        /// <summary>
        /// 根据全名称获取省级模型
        /// </summary>
        /// <param name="areaFullName">全名称:河南省,郑州市,金水区</param>
        /// <returns>河南省</returns>
        AreaViewModel GetProvinceModelFromAreaFullName(string areaFullName);

        #endregion



        #region 精确区域锁定
        /// <summary>
        /// codes转fullnames（调用自GetName）
        /// </summary>
        /// <param name="codes">11000,410101</param>
        /// <returns>北京市;河南省,郑州市,二七区</returns>

        string CodesToFullNames(string codes);


        /// <summary>
        /// 根据Code获取Area
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        AreaViewModel GetAreaViewByCode(string code);
        #endregion

        #region 区域搜索系统
        /// <summary>
        /// 区域检索引擎
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns></returns>
        List<AreaViewModel> SearchArea(string SearchKey);

        /// <summary>
        /// 区域检索引擎
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        List<AreaViewModel> SearchAreaForSubstation(string searchKey);
        #endregion

        #region 缓存

        /// <summary>
        /// 清空缓存
        /// </summary>
        void ClearCache();

        #endregion

        #region 保险

        /// <summary>
        /// 转化为保险区域
        /// </summary>
        /// <param name="areaCode">区域码</param>
        /// <returns></returns>
        string ConvertToInsuranceArea(string areaCode);

        #endregion

        #region 直辖市

        /// <summary>
        /// 是否直辖市
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        bool IsMunicipalit(string areaCode);

        #endregion
    }
}
