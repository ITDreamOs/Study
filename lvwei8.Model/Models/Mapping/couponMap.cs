using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class CouponDbModelMap : EntityTypeConfiguration<CouponDbModel>
    {
        public CouponDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.CouponId);



            // Table & Column Mappings
            this.ToTable("coupon", "lvwei8");
            /// <summary>
            /// 券卡Id
            /// </summary>
            this.Property(t => t.CouponId).HasColumnName("CouponId");
            /// <summary>
            /// 券卡名称
            /// </summary>
            this.Property(t => t.Name).HasColumnName("Name");
            /// <summary>
            /// 券卡描述
            /// </summary>
            this.Property(t => t.Desc).HasColumnName("Desc");
            /// <summary>
            /// 创建时间
            /// </summary>
            this.Property(t => t.CreateDateTime).HasColumnName("CreateDateTime");

            /// <summary>
            /// 开始时间（券的周期）
            /// </summary>
            this.Property(t => t.StartDateTime).HasColumnName("StartDateTime");

            /// <summary>
            /// 结束时间(结束时间)
            /// </summary>
            this.Property(t => t.EndDateTime).HasColumnName("EndDateTime");

            /// <summary>
            /// 长期券
            /// </summary>
            this.Property(t => t.IsPermanent).HasColumnName("IsPermanent");


            /// <summary>
            /// 面值
            /// </summary>
            this.Property(t => t.FaceValue).HasColumnName("FaceValue");

            /// <summary>
            /// 折扣率
            /// </summary>
            this.Property(t => t.DiscountRate).HasColumnName("DiscountRate");

            /// <summary>
            /// 满减 满多少
            /// </summary>
            this.Property(t => t.FullSubtractionFullValue).HasColumnName("FullSubtractionFullValue");


            /// <summary>
            /// 满减 减多少
            /// </summary>
            this.Property(t => t.FullSubtractionSubtractionValue).HasColumnName("FullSubtractionSubtractionValue");


            /// <summary>
            /// 发行量
            /// </summary>
            this.Property(t => t.Count).HasColumnName("Count");

            /// <summary>
            /// 券卡类型 0:通用优惠券 
            /// </summary>
            this.Property(t => t.CouponType).HasColumnName("CouponType");


            /// <summary>
            /// 适用商品或者服务类型
            /// </summary>
            this.Property(t => t.ProductCategory).HasColumnName("ProductCategory");

            /// <summary>
            /// 商家券
            /// </summary>
            this.Property(t => t.StoreCategory).HasColumnName("StoreCategory");
            /// <summary>
            /// 区域券
            /// </summary>
            this.Property(t => t.Areas).HasColumnName("Areas");

            /// <summary>
            /// 分站券
            /// </summary>
            this.Property(t => t.SubStationId).HasColumnName("SubStationId");
            /// <summary>
            /// 分站券
            /// </summary>
            this.Property(t => t.StoreId).HasColumnName("StoreId");

            /// <summary>
            /// 商品Id
            /// </summary>
            this.Property(t => t.ProductId).HasColumnName("ProductId");


        }
    }
}
