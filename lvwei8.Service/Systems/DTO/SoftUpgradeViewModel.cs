using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Systems.DTO
{
    /// <summary>
    /// 软件升级视图
    /// </summary>
    public class SoftUpgradeViewModel
    {
        /// <summary>
        /// 版本(数字)
        /// </summary>
        public int VersionNum { get; set; }

        /// <summary>
        /// 版本(文字)
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 是否强制升级
        /// </summary>
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 是否最新
        /// </summary>
        public bool IsLatest { get; set; }

        /// <summary>
        /// 升级内容
        /// </summary>
        public List<string> SoftUpgradeContents { get; set; }
    }
}
