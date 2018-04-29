using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class AdDbModelMap : EntityTypeConfiguration<AdDbModel>
    {
        public AdDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.AdId);

          

            // Table & Column Mappings
            this.ToTable("ad", "lvwei8");
            this.Property(t => t.AdId).HasColumnName("AdId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.AreaCode).HasColumnName("AreaCode")
                .HasMaxLength(6);
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Desc).HasColumnName("Desc");
            this.Property(t => t.link).HasColumnName("link");

        }
    }
}
