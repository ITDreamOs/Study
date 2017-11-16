using System;
using System.Collections.Generic;
namespace lvwei8.Service.UserBackend
{
    /// <summary>
    ///任务发送
    /// </summary>
    public class TaskRequest
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 用户,用|分割
        /// </summary>
        public string ListUsers {get; set;}


        /// <summary>
        /// 部门,用|分割
        /// </summary>
        public string ListPartys { get; set; }

        /// <summary>
        /// 是否安全  
        /// </summary>
        public bool IsSafe { get; set; }


    }
}