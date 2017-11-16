using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lvwei8.MvcBackend.Models
{
    public class NavItem
    {
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Roles { get; set; }
        public string LinkIco { get; set; }
        public List<NavItem> SubNavItems { get; set; }
    }
}