using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Models
{
    public class ImageServiceDbModel
    {
        /// <summary>
        /// 服务器id
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// 图片路径根目录
        /// </summary>
        public string PicRootPath { get; set; }
        /// <summary>
        /// 最大图片存储量
        /// </summary>
        public int MaxPicAmount { get; set; }
        /// <summary>
        /// 实际图片存储量
        /// </summary>
        public int CurPicAmount { get; set; }
        /// <summary>
        ///是否可用 
        /// </summary>
        public bool FlgUsable { get; set; }


    }
}