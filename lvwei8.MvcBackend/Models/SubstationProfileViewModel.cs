using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lvwei8.MvcBackend.Models
{
    public class SubstationProfileViewModel
    {

        public int UserBackendId { get; set; }
        [DisplayName("手机号")]

        [StringLength(256)]
        public String UserName { get; set; }

        [StringLength(600)]
        [DisplayName("管理的行政区")]
        //[UIHint("AdministrativeArea")]
        public String Areas { get; set; }


        [DisplayName("老客户费率")]
        [Required(ErrorMessage = "费率不能为空")]
        [RegularExpression(@"\d+(.\d{1,2})?", ErrorMessage = "最多输入两位小数")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "费率必须大于0")]
        public Decimal? ServiceRate { get; set; }

        [DisplayName("平台客户费率")]
        [Required(ErrorMessage = "费率不能为空")]
        [RegularExpression(@"\d+(.\d{1,2})?", ErrorMessage = "最多输入两位小数")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "费率必须大于0")]
        public Decimal? ServiceRateForNew { get; set; }

        [DisplayName("店铺发布特惠券费率")]
        [Required(ErrorMessage = "费率不能为空")]
        [RegularExpression(@"\d+(.\d{1,2})?", ErrorMessage = "最多输入两位小数")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "费率必须大于0")]
        public Decimal? SpreadRate { get; set; }

    }
}