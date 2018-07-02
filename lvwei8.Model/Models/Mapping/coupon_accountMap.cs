using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class CouponAccountDbModelMap : EntityTypeConfiguration<CouponAccountDbModel>
    {
        public CouponAccountDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.CouponAccountId);



            // Table & Column Mappings
            this.ToTable("coupon_account", "lvwei8");
            /// <summary>
            /// 券卡账户id
            /// </summary>
            this.Property(t => t.CouponAccountId).HasColumnName("CouponAccountId");
            /// <summary>
            /// 券卡id
            /// </summary>
            this.Property(t => t.CouponId).HasColumnName("CouponId");
            /// <summary>
            /// 用户id
            /// </summary>
            this.Property(t => t.UserId).HasColumnName("UserId");
            /// <summary>
            /// 券卡数量
            /// </summary>
            this.Property(t => t.Count).HasColumnName("Count");

            /// <summary>
            /// 创建时间(领取时间)
            /// </summary>
            this.Property(t => t.CreateDateTime).HasColumnName("CreateDateTime");

            /// <summary>
            /// 过期时间
            /// </summary>
            this.Property(t => t.EndDateTime).HasColumnName("EndDateTime");

            /// <summary>
            /// 券卡名称
            /// </summary>
            this.Property(t => t.Name).HasColumnName("Name");

            /// <summary>
            /// 描述
            /// </summary>
            this.Property(t => t.Desc).HasColumnName("Desc");

            /// <summary>
            /// 券卡使用时间
            /// </summary>
            this.Property(t => t.UseDateTIme).HasColumnName("UseDateTIme");

            /// <summary>
            /// 使用的订单id(追溯)
            /// </summary>
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            /// <summary>
            /// 适用商家
            /// </summary>
            this.Property(t => t.FitStoreId).HasColumnName("FitStoreId");

            /// <summary>
            /// 适用商家
            /// </summary>
            this.Property(t => t.FitSubStationId).HasColumnName("FitSubStationId");
            /// <summary>
            /// 适用商家
            /// </summary>
            this.Property(t => t.FitProductId).HasColumnName("FitProductId");
            /// <summary>
            /// 适用商家
            /// </summary>
            this.Property(t => t.FitAreas).HasColumnName("FitAreas");
            /// <summary>
            /// 适用商家
            /// </summary>
            this.Property(t => t.FitProductCategory).HasColumnName("FitProductCategory");

            /// <summary>
            /// 适用商家
            /// </summary>
            this.Property(t => t.FitStoreCategory).HasColumnName("FitStoreCategory");



            this.Property(t => t.FaceValue).HasColumnName("FaceValue");
        }
    }
}
