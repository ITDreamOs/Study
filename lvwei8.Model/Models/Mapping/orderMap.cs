using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class OrderDbModelMap : EntityTypeConfiguration<OrderDbModel>
    {
        public OrderDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderId);



            // Table & Column Mappings
            this.ToTable("order", "lvwei8");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
          

        }
    }
}
