
using lvwei8.Service.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base
{
    /// <summary>
    /// 过滤，排序基类
    /// </summary>
    public class FilterSortMap
    {
        /// <summary>
        /// 过滤
        /// </summary>
        public FilterMap FilterMap { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public SortMap SortMap { get; set; }
    }

    /// <summary>
    /// 过滤映射
    /// </summary>
    public class FilterMap
    {
        /// <summary>
        /// 年
        /// </summary>
        public int? Year { get; set; }
        
        /// <summary>
        /// 月
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// 汽车品牌
        /// </summary>
        public string SltCarCode { get; set; }
        /// <summary>
        /// 配件商商家类型
        /// </summary>
        public string SltAcrStoreType { get;set;}

        /// <summary>
        /// 商家类型
        /// </summary>
        public string SltStoreCategory { get; set; }

        /// <summary>
        /// 商品品牌
        /// </summary>
        public string SltGoodBrands { get; set; }

        /// <summary>
        /// 擅长领域
        /// </summary>
        public string SltGoodSkills { get; set; }

        /// <summary>
        /// 区域码：name,河南省,郑州市
        /// </summary>
        public string SltAreaCode { get; set; }

        /// <summary>
        /// 距离范围， 关联到EnumDistanceRange
        /// </summary>
        public string DistanceRange { get; set; }

        /// <summary>
        /// 通用的Key, 处理综合搜索时用
        /// </summary>
        public string District{ get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public string ProductCategory { get; set; }

        /// <summary>
        /// 配件商商品类别
        /// </summary>
        public string SltAcrProductType { get; set; }

        /// <summary>
        /// 档位
        /// </summary>
        public string GearBox { get; set; }

        /// <summary>
        /// 更多(租车对应RentalCarPublishViewModel)
        /// </summary>
        public string More { get; set; }

        /// <summary>
        /// 贵宾的车是否有故障
        /// </summary>
        public bool? IsCarEngine { get; set; }


        /// <summary>
        /// 商家联盟id
        /// </summary>
        public int? StoreAlliId { get; set; }

        #region 查找附近好友相关属性
        /// <summary>
        /// 性别 男，女
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 起始年龄
        /// </summary>
        public int AgeStart { get; set; }
        /// <summary>
        /// 最大年龄
        /// </summary>
        public int? AgeEnd { get; set; }
        /// <summary>
        /// 只查询单身
        /// </summary>
        public bool OnlySingle { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 兴趣
        /// </summary>
        public string Interest { get; set; }
        #endregion
    }

    /// <summary>
    /// 排序映射
    /// </summary>
    public class SortMap
    {
        /// <summary>
        /// 评分
        /// </summary>
        public SortType? Rating { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public SortType? Distance { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public SortType? Price { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public SortType? Date { get; set; }

        /// <summary>
        /// 活跃度
        /// </summary>
        public SortType?  Liveness { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public SortType? Sales { get; set; }
        /// <summary>
        /// 联盟成员数量
        /// </summary>
        public SortType? AlliMemberCount { get; set; }

        /// <summary>
        /// 菜单下拉类型
        /// </summary>
        public string Menu { get; set; }

        #region 代驾-
        ///// <summary>
        ///// 代驾-起点距离
        ///// </summary>
        //public SortType? DD_FromDistance { get; set; }
        ///// <summary>
        ///// 代驾-终点距离
        ///// </summary>
        //public SortType? DD_ToDistance { get; set; }
        ///// <summary>
        ///// 代驾-方向差值
        ///// </summary>
        //public SortType? DD_DirectionDValue { get; set; }
        ///// <summary>
        ///// 代驾-起始时间
        ///// </summary>
        //public SortType? DD_StartDate { get; set; }
        /// <summary>
        /// 代驾-综合因子
        /// </summary>
        public SortType? DD_All { get; set; }
        #endregion


        /// <summary>
        /// 贵宾活跃度
        /// </summary>
        public SortType? CustomerInactive { get; set; }


        /// <summary>
        /// 贵宾交易次数
        /// </summary>
        public SortType? CustomerTransaction { get; set; }


    }

    /// <summary>
    /// SortMap扩展
    /// </summary>
    public static class SortMapExtendion
    {
        public static void MakeSort(this SortMap sortMap, List<Tuple<SortType?, Action<SortType?>>> sortTypeActionMap, Action defaultSort)
        {
            if (defaultSort == null) throw new ArgumentNullException("defaultSort不能为空!");

            // 默认排序
            if (sortMap == null ||
                sortTypeActionMap == null ||
                sortTypeActionMap.Count == 0 ||
                sortTypeActionMap.Where(e => e.Item1.HasValue).Count() == 0)
            {
                defaultSort();
                return;
            }

            // 传入排序
            foreach (var mapItem in sortTypeActionMap)
            {
                if (mapItem.Item1.HasValue)
                    mapItem.Item2(mapItem.Item1);
            }
        }
    }
}
