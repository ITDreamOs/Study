using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{

    public class ProductCategoryDbModelMap : EntityTypeConfiguration<ProductCategoryDbModel>
    {
        public ProductCategoryDbModelMap()
        {

            this.HasKey(t => t.Id);
            this.ToTable("product_category", "lvwei8");

            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Desc).HasColumnName("Desc");
            this.Property(t => t.Icon).HasColumnName("Icon");

        }

    }
}
