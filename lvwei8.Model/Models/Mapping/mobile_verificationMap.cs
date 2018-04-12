using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class MobileVerificationDbModelMap : EntityTypeConfiguration<MobileVerificationDbModel>
    {
        public MobileVerificationDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.MobileVerificationId);

            // Properties
            this.Property(t => t.Mobile)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.VerificationCode)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.SendBy)
              .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("mobile_verification", "lvwei8");
            this.Property(t => t.MobileVerificationId).HasColumnName("MobileVerificationId");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.VerificationCode).HasColumnName("VerificationCode");
            this.Property(t => t.LatestSuccesGetTime).HasColumnName("LatestSuccesGetTime");
            this.Property(t => t.SendBy).HasColumnName("SendBy");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
