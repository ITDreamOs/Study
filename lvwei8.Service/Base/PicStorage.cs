using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base
{
    public class PicStorage
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 是否封面
        /// </summary>
        public bool IsCover { get; set; }
        /// <summary>
        /// 图片审核状态 EnumApplyStatus
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 备注：
        /// 1.审核失败原因
        /// </summary>
        public string Remark { get; set; }
    }
    public static class PicStorageExt
    {
        public static string SerializeToStr(this List<PicStorage> list)
        {
            if (list == null) return null;
            return JsonConvert.SerializeObject(list);
        }
        public static List<PicStorage> DeserializeListStoreage(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return JsonConvert.DeserializeObject<List<PicStorage>>(str);
        }
    }
}
