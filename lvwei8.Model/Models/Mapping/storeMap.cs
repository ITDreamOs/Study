using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class StoreDbModelMap : EntityTypeConfiguration<StoreDbModel>
    {
        public StoreDbModelMap()
        {

            this.HasKey(t => t.StoreID);
            this.ToTable("store", "lvwei8");

            this.Property(t => t.StoreID).HasColumnName("StoreID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.AddressGeoHash).HasColumnName("AddressGeoHash");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.AreaCode).HasColumnName("AreaCode");
            this.Property(t => t.AreaName).HasColumnName("AreaName");
            this.Property(t => t.OpenStartTime).HasColumnName("OpenStartTime");
            this.Property(t => t.OpenEndTime).HasColumnName("OpenEndTime");
            this.Property(t => t.BusinessLicencesPicPath).HasColumnName("BusinessLicencesPicPath");
            this.Property(t => t.SourceTypeCode).HasColumnName("SourceTypeCode");
            this.Property(t => t.ContactTel1).HasColumnName("ContactTel1");
            this.Property(t => t.ContactTel2).HasColumnName("ContactTel2");
            this.Property(t => t.ContactTel3).HasColumnName("ContactTel3");
            this.Property(t => t.ContactMobile1).HasColumnName("ContactMobile1");
            this.Property(t => t.ContactMobile2).HasColumnName("ContactMobile2");
            this.Property(t => t.ContactMobile3).HasColumnName("ContactMobile3");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.StorePics).HasColumnName("StorePics");
            this.Property(t => t.ApplyStatus).HasColumnName("ApplyStatus");
            this.Property(t => t.StoreOwnerId).HasColumnName("StoreOwnerId");
            this.Property(t => t.LastModifyDate).HasColumnName("LastModifyDate");
            this.Property(t => t.MainCategory).HasColumnName("MainCategory");
            this.Property(t => t.Categorys).HasColumnName("Categorys");
            this.Property(t => t.KeyWords).HasColumnName("KeyWords");
            this.Property(t => t.FirstLoginDate).HasColumnName("FirstLoginDate");
            this.Property(t => t.LastLoginDate).HasColumnName("LastLoginDate");
            this.Property(t => t.StoreLevel).HasColumnName("StoreLevel");

        }

    }
}
