using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models.Mapping
{
    public class UserDbModelMap : EntityTypeConfiguration<UserDbModel>
    {

        public UserDbModelMap()
        {

            this.HasKey(t => t.UserId);
            this.ToTable("users", "lvwei8");

            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.FirstLogin).HasColumnName("FirstLogin");
            this.Property(t => t.LasterLogin).HasColumnName("LasterLogin");
            this.Property(t => t.LoginTimes).HasColumnName("LoginTimes");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.NickName).HasColumnName("NickName");
            this.Property(t => t.Birth).HasColumnName("Birth");
            this.Property(t => t.AreaCode).HasColumnName("AreaCode");
            this.Property(t => t.AreaName).HasColumnName("AreaName");
            this.Property(t => t.WeiXinOpenId).HasColumnName("WeiXinOpenId");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.IdNumber).HasColumnName("IdNumber");
            this.Property(t => t.IdNumberPic).HasColumnName("IdNumberPic");
            this.Property(t => t.IdNumberBackPic).HasColumnName("IdNumberBackPic");
            this.Property(t => t.IdNumberAuthStatus).HasColumnName("IdNumberAuthStatus");
            this.Property(t => t.UplineId).HasColumnName("UplineId");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.PaymentPassword).HasColumnName("PaymentPassword");
            this.Property(t => t.BalanceHash).HasColumnName("BalanceHash");
            this.Property(t => t.PaymentPwdWrongTimes).HasColumnName("PaymentPwdWrongTimes");
            this.Property(t => t.PaymentPwdLastWrongDate).HasColumnName("PaymentPwdLastWrongDate");
            this.Property(t => t.FirstLoginStoreDate).HasColumnName("FirstLoginStoreDate");
            this.Property(t => t.LasterLoginStoreDate).HasColumnName("LasterLoginStoreDate");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.SourceType).HasColumnName("SourceType");

        }


    }
}
