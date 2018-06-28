using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class AdDbModel
    {
        public AdDbModel()
        {

        }
        /// <summary>
        /// 广告id主键
        /// </summary>
        public int AdId { get; set; }
        /// <summary>
        /// 广告类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 区域码
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string link { get; set; }
    }
}
