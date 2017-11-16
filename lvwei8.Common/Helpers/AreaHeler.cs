using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public class AreaHeler
    {
        /// <summary>
        /// 省份简拼字典
        /// </summary>

        public static readonly Dictionary<string, string> AreaAbbreviationDirctory = new Dictionary<string, string>() {
        {"北京市","京"},
        {"天津市","津"},
        {"河北省","冀"},
        {"山西省","晋"},
        {"内蒙古自治区","蒙"},
        {"辽宁省","辽"},
        {"吉林省","吉"},
        {"黑龙江省","黑"},
        {"上海市","沪"},
        {"江苏省","苏"},
        {"浙江省","浙"},
        {"安徽省","皖"},
        {"福建省","闽"},
        {"江西省","赣"},
        {"山东省","鲁"},
        {"河南省","豫"},
        {"湖北省","鄂"},
        {"湖南省","湘"},
        {"广东省","粤"},
        {"广西壮族自治区","桂"},
        {"海南省","琼"},
        {"重庆市","渝"},
        {"四川省","川"},
        {"贵州省","贵"},
        {"云南省","云"},
        {"西藏自治区","藏"},
        {"陕西省","陕"},
        {"甘肃省","甘"},
        {"青海省","青"},
        {"宁夏回族自治区","宁"},
        {"新疆维吾尔自治区","新"},

        };
        /// <summary>
        /// 省份简拼字典(不含省市)
        /// </summary>

        public static readonly Dictionary<string, string> AreaAbbreviationSimpleDirctory = new Dictionary<string, string>() {
        {"北京","京"},
        {"天津","津"},
        {"河北","冀"},
        {"山西","晋"},
        {"内蒙古","蒙"},
        {"辽宁","辽"},
        {"吉林","吉"},
        {"黑龙江","黑"},
        {"上海","沪"},
        {"江苏","苏"},
        {"浙江","浙"},
        {"安徽","皖"},
        {"福建","闽"},
        {"江西","赣"},
        {"山东","鲁"},
        {"河南","豫"},
        {"湖北","鄂"},
        {"湖南","湘"},
        {"广东","粤"},
        {"广西","桂"},
        {"海南","琼"},
        {"重庆","渝"},
        {"四川","川"},
        {"贵州","贵"},
        {"云南","云"},
        {"西藏","藏"},
        {"陕西","陕"},
        {"甘肃","甘"},
        {"青海","青"},
        {"宁夏","宁"},
        {"新疆","新"},
        };

    }
}
