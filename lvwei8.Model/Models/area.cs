using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class AreaDbModel
    {
        public AreaDbModel()
        {
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string FirstLetter { get; set; }
        public bool IsCityBlock { get; set; }
        public string FullName { get; set; }
        public string BaiduFullName { get; set; }
        public string BaiduName { get; set; }
        public string CityLevel { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<decimal> Latitude { get; set; }

    }
}
