using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class MobileVerificationDbModel
    {
        public int MobileVerificationId { get; set; }
        public string Mobile { get; set; }
        public string VerificationCode { get; set; }
        public System.DateTime LatestSuccesGetTime { get; set; }
        public int ExpiredIn { get; set; }
        public string SendBy { get; set; }
        public string Remarks { get; set; }
    }
}
