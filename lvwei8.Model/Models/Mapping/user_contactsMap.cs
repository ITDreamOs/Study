using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class UserContactsDbModelMap : EntityTypeConfiguration<UserContactsDbModel>
    {
        public UserContactsDbModelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);



            // Table & Column Mappings
            this.ToTable("user_contacts", "lvwei8");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ContactsPhone).HasColumnName("ContactsPhone");
            this.Property(t => t.ContactsUserName).HasColumnName("ContactsUserName");
            this.Property(t => t.ContactsNickName).HasColumnName("ContactsNickName");
            this.Property(t => t.ContactsIdNumber).HasColumnName("ContactsIdNumber");
            this.Property(t => t.IsChild).HasColumnName("IsChild");
         
        }
    }
}
