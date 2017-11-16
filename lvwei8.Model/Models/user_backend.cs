using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class UserBackendDbModel
    {
        public int UserBackendId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Areas { get; set; }
        public string Mobile { get; set; }
        public string Roles { get; set; }
        public Nullable<int> AgentId { get; set; }
        public System.DateTime CreateDate { get; private set; }
        public string SecurityStamp { get; set; }
        public string WeiXinAuthId { get; set; }
    }
}
