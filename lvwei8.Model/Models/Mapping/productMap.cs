using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class ProductDbModelMap : EntityTypeConfiguration<ProductDbModel>
    {
        public ProductDbModelMap()
        {
            this.HasKey(t => t.ProductId);
            this.ToTable("product", "lvwei8");

            /// <summary>
            /// 商品id
            /// </summary>
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            /// <summary>
            /// 商品名称
            /// </summary>
            this.Property(t => t.Name).HasColumnName("Name");

            /// <summary>
            /// 商品类型
            /// </summary>
            this.Property(t => t.ProductCategoryId).HasColumnName("ProductCategoryId");
            /// <summary>
            /// 商家Id
            /// </summary>
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            /// <summary>
            /// 经度
            /// </summary>
            this.Property(t => t.StoreLatitude).HasColumnName("StoreLatitude");
            /// <summary>
            /// 维度
            /// </summary>
            this.Property(t => t.StoreLongitude).HasColumnName("StoreLongitude");
            /// <summary>
            /// 所在区域的geoHash值
            /// </summary>
            this.Property(t => t.StoreAddressGeoHash).HasColumnName("StoreAddressGeoHash");
            /// <summary>
            /// 是否删除
            /// </summary>
            this.Property(t => t.IsDel).HasColumnName("IsDel");
            /// <summary>
            /// 开始时间
            /// </summary>
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            /// <summary>
            /// 结束时间
            /// </summary>
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            /// <summary>
            /// 开始时间(天)
            /// </summary>
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            /// <summary>
            /// 结束时间(天)
            /// </summary>
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            ///// <summary>
            ///// 天数（旅游周期）
            ///// </summary>
            this.Property(t => t.DateCount).HasColumnName("DateCount");
            /// <summary>
            /// 商品图片
            /// </summary>
            this.Property(t => t.Pics).HasColumnName("Pics");
            /// <summary>
            /// 经度
            /// </summary>
            this.Property(t => t.ProductLatitude).HasColumnName("ProductLatitude");
            /// <summary>
            /// 维度
            /// </summary>
            this.Property(t => t.ProductLongitude).HasColumnName("ProductLongitude");
            /// <summary>
            /// 所在区域的geoHash值
            /// </summary>
            this.Property(t => t.ProductAddressGeoHash).HasColumnName("ProductAddressGeoHash");
            /// <summary>
            /// 地点
            /// </summary>
            this.Property(t => t.ProductArea).HasColumnName("ProductArea");
            /// <summary>
            /// 主要景点（关键字)
            /// </summary>
            this.Property(t => t.KeyWords).HasColumnName("KeyWords");
            /// <summary>
            /// 排序
            /// </summary>
            this.Property(t => t.SortIndex).HasColumnName("SortIndex");
            /// <summary>
            /// 成人数量
            /// </summary>
            this.Property(t => t.AdultCount).HasColumnName("AdultCount");
            /// <summary>
            /// 成人票市场价
            /// </summary>
            this.Property(t => t.AdultTicketPrice).HasColumnName("AdultTicketPrice");
            /// <summary>
            /// 成人票售价
            /// </summary>
            this.Property(t => t.AdultPrice).HasColumnName("AdultPrice");
            /// <summary>
            /// 儿童数量
            /// </summary>
            this.Property(t => t.ChildrenCount).HasColumnName("ChildrenCount");
            /// <summary>
            /// 儿童票市场价
            /// </summary>
            this.Property(t => t.ChildrenTicketPrice).HasColumnName("ChildrenTicketPrice");
            /// <summary>
            /// 儿童票价
            /// </summary>
            this.Property(t => t.ChildrenPrice).HasColumnName("ChildrenPrice");
            /// <summary>
            /// 儿童标准描述
            /// </summary>
            this.Property(t => t.ChildrenStandardDesc).HasColumnName("ChildrenStandardDesc");
            /// <summary>
            /// 行程(用户准备的东西)或者商品描述
            /// </summary>
            this.Property(t => t.Desc).HasColumnName("Desc");
            /// <summary>
            /// 负责人用户Id
            /// </summary>
            this.Property(t => t.MasterUserId).HasColumnName("MasterUserId");
            /// <summary>
            /// 导游用户id
            /// </summary>
            this.Property(t => t.GuideUserId).HasColumnName("GuideUserId");
            /// <summary>
            /// 集合地点名称
            /// </summary>
            this.Property(t => t.DepartureAreaName).HasColumnName("DepartureAreaName");

            /// <summary>
            /// 集合地点
            /// </summary>
            this.Property(t => t.DepartureAreaCode).HasColumnName("DepartureAreaCode");

            /// <summary>
            /// 集合地经度
            /// </summary>
            this.Property(t => t.DepartureAreaLatitude).HasColumnName("DepartureAreaLatitude");
            /// <summary>
            /// 集合地维度
            /// </summary>
            this.Property(t => t.DepartureAreaLongitude).HasColumnName("DepartureAreaLongitude");
            /// <summary>
            /// 集合地所在区域的geoHash值
            /// </summary>
            this.Property(t => t.DepartureAreaAddressGeoHash).HasColumnName("DepartureAreaAddressGeoHash");
            /// <summary>
            /// 集合地点
            /// </summary>
            this.Property(t => t.DepartureStarttime).HasColumnName("DepartureStarttime");

            /// <summary>
            /// 剩余成人数量
            /// </summary>
            this.Property(t => t.RemainAdultCount).HasColumnName("RemainAdultCount");
            /// <summary>
            /// 剩余儿童数量
            /// </summary>
            this.Property(t => t.RemainChildrenCount).HasColumnName("RemainChildrenCount");
        }
    }
}