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
    public class Lvwei8MySqlReadOnlyEntities : Lvwei8MySqlEntities
    {
        public Lvwei8MySqlReadOnlyEntities() : base("Lvwei8MySqlReadOnlyEntities")
        {
            // 关闭Tracking Change
            //this.Configuration.AutoDetectChangesEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ValidateOnSaveEnabled = false;
        }
        public override int SaveChanges()
        {
            throw new Exception("不能在只读库上执行保存操作！");
        }


    }
}
