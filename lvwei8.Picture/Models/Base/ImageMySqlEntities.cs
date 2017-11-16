using lvwei8.Picture.Models.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Models.Base
{
   

    public class ImageMySqlEntities : DbContext
    {
        static ImageMySqlEntities()
        {
            Database.SetInitializer<ImageMySqlEntities>(null);
            // 启用自动迁移（应用启动时自动执行迁移脚本）
            //Database.SetInitializer<eXiuMySqlEntities>(new MigrateDatabaseToLatestVersion<eXiuMySqlEntities, Configuration>());
        }

        public ImageMySqlEntities()
            : base("Name=ImageMySqlEntities")
        {
        }

        public ImageMySqlEntities(string name)
            : base("Name=" + name)
        {
        }

        #region 配置模型
        public DbSet<ImageInfoDbModel> ImageInfo { get; set; }


        public DbSet<ImageServiceDbModel> ImageService { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ImageInfoDbModelMap());
            modelBuilder.Configurations.Add(new ImageServiceDbModelMap());
        }
        #endregion

    }
}