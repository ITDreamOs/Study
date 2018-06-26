using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 商品模型
    /// </summary>
    public partial class ProductDbModel
    {
        public ProductDbModel()
        {

        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// 商家Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public Nullable<decimal> StoreLatitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public Nullable<decimal> StoreLongitude { get; set; }

        /// <summary>
        /// 所在区域的geoHash值
        /// </summary>
        public string StoreAddressGeoHash { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 开始时间(天)
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束时间(天)
        /// </summary>
        public DateTime? EndDate { get; set; }

        ///// <summary>
        ///// 天数（旅游周期）
        ///// </summary>
        public int? DateCount { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string Pics { get; set; }


        /// <summary>
        /// 经度
        /// </summary>
        public Nullable<decimal> ProductLatitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public Nullable<decimal> ProductLongitude { get; set; }
        /// <summary>
        /// 所在区域的geoHash值
        /// </summary>
        public string ProductAddressGeoHash { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string ProductArea { get; set; }

        /// <summary>
        /// 主要景点（关键字)
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortIndex { get; set; }



        /// <summary>
        /// 成人数量
        /// </summary>
        public int? AdultCount { get; set; }

        /// <summary>
        /// 成人票市场价
        /// </summary>
        public int? AdultTicketPrice { get; set; }
        /// <summary>
        /// 成人票售价
        /// </summary>
        public int? AdultPrice { get; set; }


        /// <summary>
        /// 儿童数量
        /// </summary>
        public int? ChildrenCount { get; set; }
        /// <summary>
        /// 儿童票市场价
        /// </summary>
        public int? ChildrenTicketPrice { get; set; }
        /// <summary>
        /// 儿童票价
        /// </summary>
        public int? ChildrenPrice { get; set; }

        /// <summary>
        /// 儿童标准描述
        /// </summary>
        public string ChildrenStandardDesc { get; set; }

        /// <summary>
        /// 行程(用户准备的东西)或者商品描述
        /// </summary>
        public  string Desc { get; set; }

        /// <summary>
        /// 负责人用户Id
        /// </summary>
        public int? MasterUserId { get; set; }
        /// <summary>
        /// 导游用户id
        /// </summary>
        public int? GuideUserId { get; set; }

      



        /// <summary>
        /// 集合地点名称
        /// </summary>
        public  string DepartureAreaName { get; set; }

        /// <summary>
        /// 集合地点
        /// </summary>
        public string DepartureAreaCode { get; set; }

        /// <summary>
        /// 集合地经度
        /// </summary>
        public Nullable<decimal> DepartureAreaLatitude { get; set; }
        /// <summary>
        /// 集合地维度
        /// </summary>
        public Nullable<decimal> DepartureAreaLongitude { get; set; }

        /// <summary>
        /// 集合地所在区域的geoHash值
        /// </summary>
        public string DepartureAreaAddressGeoHash { get; set; }


        /// <summary>
        /// 集合地点
        /// </summary>
        public string DepartureStarttime { get; set; }

        /// <summary>
        /// 剩余成人数量
        /// </summary>
        public int RemainAdultCount { get; set; }



        /// <summary>
        /// 剩余儿童数量
        /// </summary>
        public int RemainChildrenCount { get; set; }


    }
}
