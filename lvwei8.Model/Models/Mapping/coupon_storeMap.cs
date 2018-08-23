using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class CouponStoreDbModelMap : EntityTypeConfiguration<CouponStoreDbModel>
    {

        public CouponStoreDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.CouponStoreId);

            // Table & Column Mappings
            this.ToTable("coupon_store", "lvwei8");
            this.Property(t => t.CouponStoreId).HasColumnName("CouponStoreId");
            this.Property(t => t.CouponId).HasColumnName("CouponId");
            this.Property(t => t.StoreId).HasColumnName("StoreId");
            this.Property(t => t.TotalCount).HasColumnName("TotalCount");
            this.Property(t => t.ReciveCount).HasColumnName("ReciveCount");
            this.Property(t => t.CouponName).HasColumnName("CouponName");
            this.Property(t => t.CouponDesc).HasColumnName("CouponDesc");
            this.Property(t => t.FaceValue).HasColumnName("FaceValue");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.IsDel).HasColumnName("IsDel");
    }
            
       




    }
}
