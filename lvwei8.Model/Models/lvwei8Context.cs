using CodeFirstStoreFunctions;
using lvwei8.Model.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class lvwei8MySqlEntities : DbContext
    {
        static lvwei8MySqlEntities()
        {
            Database.SetInitializer<lvwei8MySqlEntities>(null);
            // 启用自动迁移（应用启动时自动执行迁移脚本）
            //Database.SetInitializer<eXiuMySqlEntities>(new MigrateDatabaseToLatestVersion<eXiuMySqlEntities, Configuration>());
        }

        public lvwei8MySqlEntities()
            : base("Name=eXiuMySqlEntities")
        {
        }

        public lvwei8MySqlEntities(string name)
           : base("Name=" + name)
        {
        }

        /// <summary>
        /// 区域
        /// </summary>
        public DbSet<AreaDbModel> Areas { get; set; }

        /// <summary>
        /// 后台管理员
        /// </summary>
        public DbSet<UserBackendDbModel> UserBackend { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         
            modelBuilder.Configurations.Add(new AreaDbModelMap());
            modelBuilder.Configurations.Add(new UserBackendDbModelMap());
           
            modelBuilder.Conventions.Add(new FunctionsConvention("eXiu2", typeof(MySqlFunction)));
           
        }

    }
}
