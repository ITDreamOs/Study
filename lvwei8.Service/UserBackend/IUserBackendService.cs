using lvwei8.Model.Models;
using lvwei8.Service.UserBackend.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.UserBackend
{
    /// <summary>
    /// 后台管理账户服务
    /// </summary>
    public interface IUserBackendService
    {
        /// <summary>
        /// 查询分站
        /// </summary>
        /// <param name="totalCount">总数量</param>
        /// <param name="recentCount">最近数量</param>
        /// <param name="recentDate">最近数量计算时间起</param>
        /// <param name="groupdArea">分组区域</param>
        void QuerySubstation(out int totalCount, out int recentCount, out DateTime recentDate, out Dictionary<string, List<string>> groupdArea);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>是否存在</returns>
        bool IsExist(string userName);
        int Login(UserBackendLoginRequest param);
        void SendTaskNotify(TaskRequest task);
        /// <summary>
        /// 获取授权tokken
        /// </summary>
        /// <param name="code">用户拿到的code</param>
        /// <returns>微信认证tokken</returns>
        WeiXinAuthTokkenModel GetWeiXinAuthToken(string code);
        /// <summary>
        ///获取用户id 
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>获取用户</returns>
        string GetWeixinAuthUserId(string code);

        /// <summary>
        /// 微信是否已经认证
        /// </summary>
        /// <param name="code">用户code</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        WeixinAuthUserStatus IsWeiXinAuth(string code);

        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        UserBackendDbModel Get(int userid);


        #region 回调机制
        #region 获取ip
        /// <summary>
        /// 获取微信ip
        /// </summary>
        /// <returns></returns>
        List<string> GetWeiIxnIp();
        #endregion



        #region 连接ip并响应给微信
        /// <summary>
        /// 连接微信ip 并推送响应消息
        /// </summary>
        /// <param name="message">连接微信ip 并推送响应消息</param>
        void WeiXinClientMessage(string message);
        #endregion

        #region 验证URL有效性
        /// <summary>
        /// 验证回调URL有效性
        /// </summary>
        /// <param name="requestauth">微信请求体</param>
        string WeiXinAuthURL(WeiXinCallBackAuthViewModel requestauth);
        #endregion

        #region 消息命令处理
        /// <summary>
        /// 消息命令处理
        /// </summary>
        /// <param name="weixincontent">微信消息</param>
        /// <param name="RegStr">微信消息</param>
        /// <returns></returns>
        string WeixinContentFillter(string weixincontent, string RegStr);
        #endregion

        #region 信息转发处理
        /// <summary>
        /// 信息转发处理
        /// </summary>
        /// <param name="request">请求处理</param>
        /// <param name="requestream">请求流</param>
        /// <returns></returns>
        string SendCallBackMessage(WeiXinCallBackAuthViewModel request, Stream requestream);
        #endregion
        #endregion

        #region 微信成员管理
        #region 获取成员信息
        /// <summary>
        /// 通过微信id获取微信的用户详细信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        WeiXinUserViewModel GetWeiXinUserByWeixinUserId(string userid);

        #endregion
        #endregion


        #region 公众号 

        #region 获取用户oppid和token
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        WeiXinAuthTokkenModel GetOpenWeiXinAuthToken(string code);
        #endregion


        #region 检测tokken
        /// <summary>
        /// 检测token合法性
        /// </summary>
        /// <param name="openid">公众号用户id</param>
        /// <param name="access_token">token</param>
        /// <returns></returns>
        bool checkToken(string openid,string access_token);
        #endregion

        #endregion
    }
}
