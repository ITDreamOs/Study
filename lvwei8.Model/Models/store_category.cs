using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 商家类型
    /// </summary>
    public partial class StoreCategoryDbModel
    {
        public StoreCategoryDbModel()
        {

        }
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Icon { get; set; }


    }
}
