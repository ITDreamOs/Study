using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.UserBackend.DTO
{
    /// <summary>
    /// 微信回调模型
    /// </summary>
    public class WeiXinCallBackAuthViewModel
    {
        /// <summary>
        /// 微信加密签名，msg_signature结合了企业填写的token、请求中的timestamp、nonce参数、加密的消息体
        /// </summary>
        public string msg_signature { get; set; }
        /// <summary>
        /// 	时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string nonce  { get; set; }
        /// <summary>
        /// 加密的随机字符串，以msg_encrypt格式提供。需要解密并返回echostr明文，解密后有random、msg_len、msg、$CorpID四个字段，其中msg即为echostr明文
        /// </summary>
        public string echostr { get; set; }


    }

    /// <summary>
    /// 微信服务器的ip段模型
    /// </summary>
    public class WeixinIPViewModel
    {
        /// <summary>
        /// 微信ip段
        /// </summary>
        public List<string> ip_list { get; set; }

    }

    /// <summary>
    /// 用户转账模型
    /// </summary>
    public class WxinxinUserTransferViewModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 转账描述
        /// </summary>
        public string AmountDesc { get; set; }

        /// <summary>
        /// 订单唯一标识
        /// </summary>
        public string Token { get; set; }


    }

}
