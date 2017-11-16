using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Models.Map
{


    public class ImageServiceDbModelMap : EntityTypeConfiguration<ImageServiceDbModel>
    {
        public ImageServiceDbModelMap()
        {
            this.HasKey(t => t.ServerId);
            this.ToTable("ImageService", "lvwei8img");
            this.Property(t => t.ServerId).HasColumnName("ServerId");
            this.Property(t => t.ServerName).HasColumnName("ServerName");
            this.Property(t => t.ServerUrl).HasColumnName("ServerUrl");
            this.Property(t => t.PicRootPath).HasColumnName("PicRootPath");
            this.Property(t => t.CurPicAmount).HasColumnName("CurPicAmount");
            this.Property(t => t.MaxPicAmount).HasColumnName("MaxPicAmount");
            this.Property(t => t.FlgUsable).HasColumnName("FlgUsable");
        }
    }
}