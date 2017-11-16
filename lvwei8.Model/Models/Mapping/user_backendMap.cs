using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class UserBackendDbModelMap : EntityTypeConfiguration<UserBackendDbModel>
    {
        public UserBackendDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.UserBackendId);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Areas)
                .HasMaxLength(600);



            this.Property(t => t.Mobile)
                .HasMaxLength(22);

            this.Property(t => t.Roles)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.SecurityStamp)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("user_backend", "exiu_test");
            this.Property(t => t.UserBackendId).HasColumnName("UserBackendId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Areas).HasColumnName("Areas");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Roles).HasColumnName("Roles");
            this.Property(t => t.AgentId).HasColumnName("AgentId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.WeiXinAuthId).HasColumnName("WeiXinAuthId");
        }
    }
}
