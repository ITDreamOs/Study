using lvwei8.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Systems.DTO
{
    /// <summary>
    /// 获取最新软件请求
    /// </summary>
    public class GetLastedSoftRequest
    {
        /// <summary>
        /// 获取软件最新版本
        /// </summary>
        public TerminalSource SoftId { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int VersionNum { get; set; }
    }
}
