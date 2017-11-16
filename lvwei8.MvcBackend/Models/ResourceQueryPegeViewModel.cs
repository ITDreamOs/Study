using lvwei8.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lvwei8.MvcBackend.Models
{
    /// <summary>
    /// 资源分页查询
    /// </summary>
    public class ResourceQueryPegeViewModel
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int? PageNo { get; set; }
        /// <summary>
        /// 一页多少
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 终端来源
        /// </summary>
        public TerminalSource? TerminalSource { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool? IsReply { get; set; }
        /// <summary>
        /// 区域码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary>
        public int? AdType { get; set; }


        /// <summary>
        /// 是否打印
        /// </summary>
        public string IsPrint { get; set; }

        /// <summary>
        /// 结果类型
        /// </summary>
        public int? NLPValueType { get; set; }
        /// <summary>
        /// qq群号
        /// </summary>
        public int? QQGroupKey { get; set; }
        /// <summary>
        /// 区域关键字
        /// </summary>
        public string AreaKeyWords { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}