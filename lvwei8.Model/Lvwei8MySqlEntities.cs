using lvwei8.Model.Models;
using lvwei8.Model.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model
{
    public class Lvwei8MySqlEntities: DbContext
    {
        static Lvwei8MySqlEntities()
        {
            Database.SetInitializer<Lvwei8MySqlEntities>(null);
            // 启用自动迁移（应用启动时自动执行迁移脚本）
            //Database.SetInitializer<eXiuMySqlEntities>(new MigrateDatabaseToLatestVersion<eXiuMySqlEntities, Configuration>());
        }

        public Lvwei8MySqlEntities()
            : base("Name=Lvwei8MySqlEntities")
        {
        }

        public Lvwei8MySqlEntities(string name)
            : base("Name=" + name)
        {
        }

        #region 配置模型
        public DbSet<AreaDbModel> Areas { get; set; }

        public DbSet<UserBackendDbModel> UserBackend { get; set; }

        public DbSet<UserDbModel> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new AreaDbModelMap());
            modelBuilder.Configurations.Add(new UserBackendDbModelMap());
            modelBuilder.Configurations.Add(new UserDbModelMap());
        }
        #endregion

    }
}
