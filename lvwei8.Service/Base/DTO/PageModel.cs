﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base
{
    public class PageModel
    {
        /// <summary>
        /// 从1开始的页码
        /// </summary>
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public static PageModel Default()
        {
            return new PageModel()
            {
                PageNo = 1,
                PageSize = 10
            };
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        private int _recordCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set
            {
                this._recordCount = value;
                this.PageSize = this.PageSize <= 0 ? 10 : this.PageSize;
                this.PageCount = this.RecordCount % this.PageSize == 0 ? this.RecordCount / this.PageSize : (this.RecordCount / this.PageSize + 1);
                //this.PageNo = this.PageNo > this.PageCount ? this.PageCount : this.PageNo;
                this.PageNo = this.PageNo < 1 ? 1 : this.PageNo;
            }
        }

        /// <summary>
        /// Skip
        /// </summary>
        [JsonIgnore]
        public int Skip
        {
            get
            {
                return (this.PageNo - 1) * this.PageSize;
            }
        }
        [JsonIgnore]
        public bool CurrentPageIsEmpty
        {
            get { return RecordCount <= Skip; }
        }

        [JsonIgnore]
        public bool CurrentPageIsFull
        {
            get { return (RecordCount - Skip) >= PageSize; }
        }
        #region 扩展属性
        /// <summary>
        /// 扩展属性
        /// </summary>
        public string PageExtend { get; set; }
        #endregion
    }
}
