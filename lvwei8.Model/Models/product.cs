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
        /// 
        /// </summary>
        public string LineMaster { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LineMasterTel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LineMasterMoble { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Leader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LeaderMobil { get; set; }

        /// <summary>
        /// 集合地点
        /// </summary>
        public string JIheTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JiHePlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Bianhao { get; set; }

    }
}
