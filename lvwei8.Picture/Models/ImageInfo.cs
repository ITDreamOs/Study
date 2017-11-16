using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Models
{
    public class ImageInfoDbModel
    {
        /// <summary>
        /// 图片主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// 图片服务器id
        /// </summary>
        public int ImageServerId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

 

    }
}