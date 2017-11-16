using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lvwei8.MvcBackend.Common
{
    public class WashcarUser
    {
        public string UserName { get; set; }
        public string CarNumber { get; set; }
        public string PhoneNum { get; set; }
        public int RemainTimes { get; set; }
    }
}