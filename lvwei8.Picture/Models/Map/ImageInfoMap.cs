using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Models.Map
{

    public class ImageInfoDbModelMap : EntityTypeConfiguration<ImageInfoDbModel>
    {
        public ImageInfoDbModelMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("ImageInfo", "lvwei8img");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.ImageServerId).HasColumnName("ImageServerId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }


    }

}