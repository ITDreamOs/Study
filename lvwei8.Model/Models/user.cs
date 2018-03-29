using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class UserDbModel
    {
        public UserDbModel()
        {

        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDate { get; private set; }

        /// <summary>
        /// 首次登陆时间
        /// </summary>
        public Nullable<System.DateTime> FirstLogin { get; set; }
        /// <summary>
        /// 最近一次登陆时间
        /// </summary>
        public Nullable<System.DateTime> LasterLogin { get; set; }

        /// <summary>
        /// 登陆次数
        /// </summary>
        public Nullable<int> LoginTimes { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 真实名字
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public Nullable<System.DateTime> Birth { get; set; }

        /// <summary>
        /// 区域码
        /// </summary>
        private string _areaCode;
        /// <summary>
        /// 常驻区域码
        /// </summary>
        public string AreaCode
        {
            get
            {
                if (string.IsNullOrEmpty(_areaCode))
                {
                    return "310000";//为空时返回上海市
                }
                else
                {
                    return _areaCode;
                }
            }
            set { _areaCode = value; }
        }
        /// <summary>
        /// 常驻区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        public string WeiXinOpenId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// 身份证正面图片
        /// </summary>
        public string IdNumberPic { get; set; }
        /// <summary>
        /// 身份证背面图片
        /// </summary>
        public string IdNumberBackPic { get; set; }
        /// <summary>
        /// 身份证认证状态
        /// </summary>
        public string IdNumberAuthStatus { get; set; }
        /// <summary>
        /// 上线id
        /// </summary>
        public Nullable<int> UplineId { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 支付密码
        /// </summary>
        public string PaymentPassword { get; set; }
        /// <summary>
        /// 余额hash
        /// </summary>
        public string BalanceHash { get; set; }
        /// <summary>
        /// 支付密码错误次数
        /// </summary>
        public int PaymentPwdWrongTimes { get; set; }
        /// <summary>
        /// 支付密码错误最近的时间
        /// </summary>
        public Nullable<System.DateTime> PaymentPwdLastWrongDate { get; set; }

        /// <summary>
        /// 第一次登陆店铺的时间
        /// </summary>
        public DateTime? FirstLoginStoreDate { get; set; }
        /// <summary>
        /// 最近一次登陆店铺的时间
        /// </summary>
        public DateTime? LasterLoginStoreDate { get; set; }

        /// <summary>
        /// 用户分数,分数作为vip等级的依据
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public int? SourceType { get; set; }
    }

}
