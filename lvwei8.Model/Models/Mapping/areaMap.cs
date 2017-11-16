using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class AreaDbModelMap : EntityTypeConfiguration<AreaDbModel>
    {
        public AreaDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.FirstLetter)
                .HasMaxLength(1);

            this.Property(t => t.FullName)
                .HasMaxLength(90);

            this.Property(t => t.BaiduFullName)
                .HasMaxLength(90);

            this.Property(t => t.BaiduName)
                .HasMaxLength(30);


            // Table & Column Mappings
            this.ToTable("area", "lvwei8");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FirstLetter).HasColumnName("FirstLetter");
            this.Property(t => t.IsCityBlock).HasColumnName("IsCityBlock");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.BaiduFullName).HasColumnName("BaiduFullName");
            this.Property(t => t.BaiduName).HasColumnName("BaiduName");
            this.Property(t => t.CityLevel).HasColumnName("CityLevel");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            
        }
    }
}
