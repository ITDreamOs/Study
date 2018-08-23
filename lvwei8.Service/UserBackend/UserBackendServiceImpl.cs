using lvwei8.Model.Models;
using lvwei8.Service.Account;
using lvwei8.Service.Area;
using lvwei8.Service.UserBackend.DTO;
using lvwei8.Service.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace lvwei8.Service.UserBackend
{
    /// <summary>
    /// 用户后台账户服务
    /// </summary>
    public class UserBackendServiceImpl : BaseContextServiceImpl, IUserBackendService
    {
        #region 属性依赖

        /// <summary>
        /// 用户后台账户仓储
        /// </summary>
        public IReadOnlyRepository<UserBackendDbModel> ReadOnlyRepository { get; set; }
        /// <summary>
        /// 区域服务
        /// </summary>
        public IAreaService AreaService { get; set; }




        /// <summary>
        /// 账户服务
        /// </summary>
        public IAccountService AccountService { get; set; }

     


        #endregion

        #region 微信企业号支持
        //企业id
        private readonly static string CORPID = "wxe4004af855353d40";
        //企业secret
        private readonly static string CORPSECRET = "mVbn7l1UrEX8IX3lArAJnlMmQOxx1O6NQlf0dNAtZ3NZzKYCFpqIyMZCfs74XrcZ";
        //回调Token
        private readonly static string CALLBACKTOKEN = "qukhSeWKlVYcBRrW9vNr";
        //EncodingAESKey
        private readonly static string CALLBACKENCODINGAESKEY = "A8tDDqgw9vhkQfPpc6otmLuBOGUxIkeQJWfMSsf9Mqm";

        #endregion

        #region 微信公众号支持

        #region 支付公众号
        //企业公众号id
        private readonly string OPENCORPID = "wxfb87677213aa8675";
        //企业公众号secret
        private readonly string OPENCORPSECRET = "1b1c42ba88a7154512e3ce28cdb60489";
        //服务器token
        private readonly string OPENCALLBACKTOKEN = "298029_b";
        //服务器EncodingAESKey
        private readonly string OPENCALLBACKENCODINGAESKEY = "GATmdgITGERoRSS5vx8f8GB94zUeBe3h1GCsM4yVulP";

        //认证url https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxfb87677213aa8675&redirect_uri=https://testwww.114995.com/Account/Register&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirec
        #endregion

        #region 注册公众号
        //企业公众号id
        private readonly string RegisterOPENCORPID = "wx03e7fba2f52db4e0";
        //企业公众号secret
        private readonly string RegisterOPENCORPSECRET = "9bed9e6ea0fde6e5d85102f782bcc146";
        //服务器token
        private readonly string RegisterOPENCALLBACKTOKEN = "298029_b";
        //服务器EncodingAESKey
        private readonly string RegisterOPENCALLBACKENCODINGAESKEY = "GATmdgITGERoRSS5vx8f8GB94zUeBe3h1GCsM4yVulP";
        #endregion

        #endregion

        #region 接口

        /// <summary>
        /// 查询分站
        /// </summary>
        /// <param name="totalCount">总数量</param>
        /// <param name="recentCount">最近数量</param>
        /// <param name="recentDate">最近数量计算时间起</param>
        /// <param name="groupdArea">分组区域</param>
        public void QuerySubstation(out int totalCount, out int recentCount, out DateTime recentDate, out Dictionary<string, List<string>> groupdArea)
        {
            string subStationRole = "SubStation";
            IQueryable<UserBackendDbModel> query = ReadOnlyRepository.Query(e => e.Roles.Contains(subStationRole));
            var dbResult = query.Select(e => new { Areas = e.Areas, CreateDate = e.CreateDate, });
            HashSet<string> areas = new HashSet<string>();
            foreach (var area in dbResult)
            {
                foreach (var item in area.Areas.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    areas.Add(item);
                }
            }
            //City = AreaService.GetCityModelFromAreaCode(e).Name
            var substationModel = areas.OrderBy(e => e).Select(e => new
            {
                Code = e,
                /*
                 Povince = AreaService.GetAreaName(e.Substring(0, 2) + "0000"),
                City = AreaService.GetAreaName(e),
                 */
                Povince = AreaService.GetProvinceModelFromCode(e).Name,
                City = AreaService.GetCityModelFromAreaCode(e).Name +
                (e.EndsWith("00") ? "" : AreaService.GetAreaViewByCode(e).Name),
            });

            totalCount = substationModel.Count();
            var recentD = DateTime.Now.AddDays(-7);
            recentCount = dbResult.Where(e => e.CreateDate > recentD).Count();
            groupdArea = substationModel.GroupBy(e => e.Povince).ToDictionary(g => g.Key, g => g.ToList().Select(e => e.City).ToList());
            recentDate = dbResult.OrderByDescending(e => e.CreateDate).Select(e => e.CreateDate).FirstOrDefault();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>是否存在</returns>
        public bool IsExist(string userName)
        {
            return ReadOnlyRepository.Count(e => e.UserName == userName) > 0;
        }

        public int Login(UserBackendLoginRequest param)
        {
            throw new NotImplementedException();
        }
        public void SendTaskNotify(TaskRequest task)
        {
            SendTaskNotifyStatic(task);
        }
        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="task"></param>
        public static void SendTaskNotifyStatic(TaskRequest task)
        {            
            var message = new WeiXinSendMessage();
            message.msgtype = "text";
            message.safe = task.IsSafe ? "1" : "0";
            if (!string.IsNullOrWhiteSpace(task.ListUsers))
            {
                message.touser = task.ListUsers;
            }
            if (!string.IsNullOrWhiteSpace(task.ListPartys))
            {
                message.toparty = task.ListPartys;
            }

            if (string.IsNullOrWhiteSpace(task.ListUsers) && string.IsNullOrWhiteSpace(task.ListPartys))
            {
                message.toparty = "5";
            }

            message.agentid = 0;
            message.text = new Text()
            {
                content = task.Content,
            };
            var CacheTokken = GetCaCheTokken();
            var sendresult = SendWeiXinMsg(message, CacheTokken);
            //如果失败，尝试重置token，再试一次
            if (!sendresult)
            {
                if (HttpRuntime.Cache["WeiXinTokken"] != null)
                {
                    HttpRuntime.Cache.Remove("WeiXinTokken");
                }
                CacheTokken = GetCaCheTokken();
                sendresult = SendWeiXinMsg(message, CacheTokken);
            }
        }

        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public UserBackendDbModel Get(int userid)
        {
            return ReadOnlyRepository.Get(e => e.UserBackendId == userid);
        }

        #endregion

        #region 企业号
        #region 微信消息
        #region 发送微信消息
        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <returns></returns>
        private static bool SendWeiXinMsg(WeiXinSendMessage message, string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken)) return false;
            //发送请求的url
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", AccessToken);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            var requestbody = JsonConvert.SerializeObject(message);
            byte[] byteArray = Encoding.UTF8.GetBytes(requestbody);
            request.ContentLength = byteArray.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            //发请求
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var status = JObject.Parse(result).ToObject<WeiXinStatus>();
            return status.ErrMsg == "ok" ? true : false;
        }
        #endregion

        #region 获取缓存Tokken
        private static object syncTokenRoot = new Object();
        /// <summary>
        /// 获取缓存Tokken
        /// </summary>
        /// <returns></returns>
        private static string GetCaCheTokken()
        {
            if (HttpRuntime.Cache["WeiXinTokken"] == null)
            {
                lock (syncTokenRoot)
                {
                    if (HttpRuntime.Cache["WeiXinTokken"] == null)
                    {
                        var result = GetTokken();
                        HttpRuntime.Cache.Insert("WeiXinTokken", result, null, DateTime.Now.AddSeconds(5400), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                }
            }
            return (string)HttpRuntime.Cache["WeiXinTokken"];
        }
        #endregion

        #region 获取Tokken

        /// <summary>
        /// 获取微信tokken
        /// </summary>
        /// <returns></returns>
        private static string GetTokken()
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", CORPID, CORPSECRET);
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result);
            var status = JsonResult.ToObject<WeiXinStatus>();            
            var jsontokken = JsonResult.ToObject<WeiXinAccessToken>();
            return jsontokken.Access_Token;
        }

        #endregion
        #endregion

        #region 微信企业号认证

        #region 第一步code 换取tokken和openid
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WeiXinAuthTokkenModel GetWeiXinAuthToken(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                //重新获取code
            }
            var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", CORPID, CORPSECRET, code);
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result);
            var status = JsonResult.ToObject<WeiXinStatus>();
            if (!string.IsNullOrEmpty(status.ErrMsg))
            {
                //重新获取code
            }
            var jsontokken = JsonResult.ToObject<WeiXinAuthTokkenModel>();
            return jsontokken;
        }
        #endregion


        #region  获取用户id
        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        public string GetWeixinAuthUserId(string code)
        {
            if (string.IsNullOrEmpty(code)) return string.Empty;
            var token = GetCaCheTokken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}&agentid={2}", token, code, "0");
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result);
            var status = JsonResult.ToObject<WeiXinStatus>();
            if (!string.IsNullOrEmpty(status.ErrMsg)) return string.Empty;
            var jsontokken = JsonResult.ToObject<WeiXinAuthUserModel>();
            if (jsontokken == null) return string.Empty;
            if (!string.IsNullOrEmpty(jsontokken.UserId)) return jsontokken.UserId;
            return string.Empty;
        }
        #endregion

        #region 是否微信已经认证过
        /// <summary>
        /// 是否微信已经认证过
        /// </summary>
        /// <param name="code">用户code</param>
        /// <returns></returns>
        public WeixinAuthUserStatus IsWeiXinAuth(string code)
        {
            var weixinauthresult = new WeixinAuthUserStatus() { AuthStatus = false, UserId = null };
            if (string.IsNullOrEmpty(code)) return weixinauthresult;
            var weixinuserid = GetWeixinAuthUserId(code);
            if (string.IsNullOrEmpty(weixinuserid)) return weixinauthresult;
            var user = ReadOnlyRepository.Get(e => e.WeiXinAuthId == weixinuserid);
            if (user == null) return weixinauthresult;
            weixinauthresult.AuthStatus = true;
            weixinauthresult.UserId = user.UserBackendId;
            return weixinauthresult;
        }
        #endregion

        #endregion

        #region 微信回调

        #region 验证URL有效性
        /// <summary>
        /// 验证回调URL有效性
        /// </summary>
        /// <param name="requestauth">微信请求体</param>
        public string WeiXinAuthURL(WeiXinCallBackAuthViewModel requestauth)
        {
           // Infrastructure.WeiXin.WXBizMsgCrypt wxcpt = new Infrastructure.WeiXin.WXBizMsgCrypt(CALLBACKTOKEN, CALLBACKENCODINGAESKEY, CORPID);
            string sVerifyMsgSig = requestauth.msg_signature;
            string sVerifyTimeStamp = requestauth.timestamp;
            string sVerifyNonce = requestauth.nonce;
            string sVerifyEchoStr = requestauth.echostr;
            int ret = -1;
            string sEchoStr = "";
          //  ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            if (ret == 0)
            {
                return JsonConvert.SerializeObject(new WeiXinMessageResult { Status = true, Result = sEchoStr, Code = ret.ToString() });
            }
            return JsonConvert.SerializeObject(new WeiXinMessageResult { Status = false, Result = "", Code = ret.ToString() });
        }
        #endregion


        #region 信息转发处理
        /// <summary>
        /// 信息转发处理
        /// </summary>
        /// <param name="request">请求处理</param>
        /// <param name="requestream">请求流</param>
        /// <returns></returns>
        public string SendCallBackMessage(WeiXinCallBackAuthViewModel request, Stream requestream)
        {

          //  Infrastructure.WeiXin.WXBizMsgCrypt wxcpt = new Infrastructure.WeiXin.WXBizMsgCrypt(CALLBACKTOKEN, CALLBACKENCODINGAESKEY, CORPID);
            int ret = -1;
            string sMsg = "";  //解析之后的明文
            string sReqData = GetXml(requestream);
           // ret = wxcpt.DecryptMsg(request.msg_signature, request.timestamp, request.nonce, sReqData, ref sMsg);
            if (ret != 0)
            {
                return JsonConvert.SerializeObject(new WeiXinMessageResult { Status = false, Result = "", Code = ret.ToString() });
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sMsg);


            XmlNode cropid = doc.SelectSingleNode("//ToUserName");
            string cropId = cropid.InnerText;//获取企业id
            XmlNode from = doc.SelectSingleNode("//FromUserName");
            string userid = from.InnerText;//获取员工id
            XmlNode type = doc.SelectSingleNode("//MsgType");
            string msgtype = type.InnerText;//操作的类型
            XmlNode CreateTime = doc.SelectSingleNode("//CreateTime");
            string createtime = CreateTime.InnerText;//消息的创建时间

            XmlNode AgentID = doc.SelectSingleNode("//AgentID");
            string agentid = AgentID.InnerText;//应用id     

            var Content = string.Empty;
            if (msgtype == "text")
            {
                XmlNode content = doc.SelectSingleNode("//Content");
                Content = content.InnerText;
            }
            else if (msgtype == "event")
            {
                Content = "事件";
            }

            var SendContent = string.Empty;
            #region 监控发给谁
            #region 能力判断
            ////修改支付密码能力的
            //var IsChangePayPassWordPowers = Service.Enums.WeiXinTaskRegStr.ChangePayPassWordPower.Split(',').ToList().Contains(userid);
            ////修改密码
            //var IsChangePassWordPower = Service.Enums.WeiXinTaskRegStr.ChangePassWordPower.Split(',').ToList().Contains(userid);
            ////查询用户id
            //var IsSearchUser = Service.Enums.WeiXinTaskRegStr.SearchUserIdPower.Split(',').ToList().Contains(userid);
            //#endregion
            //var isPower = (IsChangePayPassWordPowers || IsChangePassWordPower)||IsSearchUser;
            //获取能力
            var UserPower = GetPowerByWeixinUserId(userid);
            if (UserPower.IsPower)
            {
                if (msgtype == "text")
                {
                    #region 帮助功能
                    //根据帮助文档获取
                    var HelpTxt = WeixinPowerHelpText(UserPower);
                    #endregion
                    #region 获取sendContent

                    SendContent = getSendContent(Content, HelpTxt, UserPower);

                    #endregion

                    //#region 修改支付密码
                    ////修改支付密码
                    //if (IsChangePayPassWordPowers)
                    //{
                    //    if (Common.Helpers.StringHelper.StrIsMatch(Content, Service.Enums.WeiXinTaskRegStr.ChangePayPassword).Success)
                    //    {
                    //        SendContent = WeixinContentFillter(Content, Service.Enums.WeiXinTaskRegStr.ChangePayPassword);
                    //    }
                    //    else if (Content == "TX")
                    //    {
                    //        WalletService.WithdrawReady();
                    //        SendContent = "提现信息已发送!:)";
                    //    }
                    //    else if (Content == "支付密码")
                    //    {
                    //        SendContent = Service.Enums.WeiXinTaskRegStr.ChangePayPasswordDsec;
                    //    }
                    //    else
                    //    {
                    //        //SendContent = IsChangePassWordPower ? HelpTxt : "修改支付密码请回复：支付密码 4个字,系统将给你提供修改规则";
                    //        SendContent = HelpTxt;
                    //    }
                    //}
                    //#endregion

                    //#region 修改密码
                    ////修改密码
                    //if (IsChangePassWordPower)
                    //{
                    //    if (Common.Helpers.StringHelper.StrIsMatch(Content, Service.Enums.WeiXinTaskRegStr.ChangePassword).Success)
                    //    {
                    //        SendContent = ChangePassword(Content, Service.Enums.WeiXinTaskRegStr.ChangePassword, Service.Enums.WeiXinTaskRegStr.ChangePasswordDsec);
                    //    }
                    //    else if (Content == "修改密码")
                    //    {
                    //        SendContent = Service.Enums.WeiXinTaskRegStr.ChangePasswordDsec;
                    //    }
                    //    else
                    //    {
                    //        //SendContent = IsChangePayPassWordPowers ? HelpTxt : "修改密码请回复：修改密码 4个字,系统将给你提供修改规则";
                    //        SendContent = HelpTxt;
                    //    }
                    //}
                    //#endregion

                    //#region 查询用户Id
                    ////查询用户ID
                    //if (IsSearchUser)
                    //{
                    //    if (Common.Helpers.StringHelper.StrIsMatch(Content, Service.Enums.WeiXinTaskRegStr.SearchUserId).Success)
                    //    {
                    //        SendContent = GetUser(Content, Service.Enums.WeiXinTaskRegStr.SearchUserId, Service.Enums.WeiXinTaskRegStr.SearchUserIdDsec);
                    //    }
                    //    else if (Content == "查询用户")
                    //    {
                    //        SendContent = Service.Enums.WeiXinTaskRegStr.SearchUserIdDsec;
                    //    }
                    //    else
                    //    {
                    //        // SendContent = IsSearchUser ? "1:如果想修改密码请回复：修改密码 4个字,系统将给你提供修改规则;<br/>2:如果想修改支付密码请回复：支付密码 4个字,系统将给你提供修改规则;" : "修改密码请回复：修改密码 4个字,系统将给你提供修改规则";
                    //        SendContent = HelpTxt;
                    //    }
                    //}
                    //#endregion
                }
                else if (msgtype == "event")
                {

                    XmlNode events = doc.SelectSingleNode("//Event");
                    string weixinevents = events.InnerText; //事件类型

                    XmlNode EventKey = doc.SelectSingleNode("//EventKey");
                    string weiXinEventKey = EventKey.InnerText;//事件KEY值


                    XmlNode ScanCodeInfo = doc.SelectSingleNode("//ScanCodeInfo");
                    string weiXinScanCodeInfo = ScanCodeInfo.InnerText;  //扫描信息

                    var weiXinScanType = string.Empty;
                    var weiXinScanResult = string.Empty;


                    if (!string.IsNullOrEmpty(weiXinScanCodeInfo))
                    {
                        XmlNode ScanType = doc.SelectSingleNode("//ScanType");
                        weiXinScanType = ScanType.InnerText;//扫描类型
                        XmlNode ScanResult = doc.SelectSingleNode("//ScanResult");
                        weiXinScanResult = ScanResult.InnerText;//扫描结果
                    }

                    if (string.IsNullOrEmpty(weiXinScanResult))
                    {
                        SendContent = "扫码信息有误!";
                    }
                    else
                    {
                        var usertransfer = JsonConvert.DeserializeObject<WxinxinUserTransferViewModel>(weiXinScanResult);
                        if (usertransfer == null)
                        {
                            SendContent = "扫码信息有误!";
                        }
                        else
                        {
                            SendTaskNotify(new TaskRequest() { Content = usertransfer.UserId + "amount:" + usertransfer.Amount + "desc:" + usertransfer.AmountDesc, ListUsers = "lizf@114995.com" });
                           // WalletService.Transfer(ITradeTypeConfigurationExtention.ExiuAsUserId, usertransfer.UserId, usertransfer.Amount, "", usertransfer.AmountDesc, usertransfer.Token);
                            SendContent = "转账已完成!";
                        }
                    }
                }
            }
            SendContent = (!UserPower.IsPower) ? "你好,我是驴尾巴!好好工作,不要逗我玩!" : (string.IsNullOrEmpty(Content)) ? "不知道你说的什么!" : SendContent;
            #endregion
            var CallBackEncryptMsg = string.Empty;
            string result = SetData(SendContent, userid, cropId, createtime);
            if (msgtype == "text" || msgtype == "event")//如果接收的是消息类型
            {
             //   ret = wxcpt.EncryptMsg(result, request.timestamp, request.nonce, ref CallBackEncryptMsg);
                if (ret != 0)
                {
                    return JsonConvert.SerializeObject(new WeiXinMessageResult { Status = false, Result = "", Code = ret.ToString() });
                }
                return JsonConvert.SerializeObject(new WeiXinMessageResult { Status = true, Result = CallBackEncryptMsg, Code = ret.ToString() });
            }
            return JsonConvert.SerializeObject(new WeiXinMessageResult { Status = true, Result = "未知的操作", Code = ret.ToString() });
        }
        #endregion

        #region 微信成员管理
        #region 获取成员
        /// <summary>
        /// 通过微信id获取微信的用户详细信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public WeiXinUserViewModel GetWeiXinUserByWeixinUserId(string userid)
        {

            if (string.IsNullOrEmpty(userid))
            {
                return null;
            }
            var token = GetCaCheTokken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", token, userid);
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result).ToObject<WeiXinUserViewModel>();
            return JsonResult;
        }

        #endregion
        #endregion

        #region 获取认证ip
        /// <summary>
        /// 获取微信服务器ip段
        /// </summary>
        /// <returns></returns>
        public List<string> GetWeiIxnIp()
        {
            var tokken = GetCaCheTokken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", tokken);
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result);

            var status = JsonResult.ToObject<WeiXinStatus>();
            if (!string.IsNullOrEmpty(status.ErrMsg))
            {
                GetWeiIxnIp();
            }
            var weixinIps = JsonResult.ToObject<WeixinIPViewModel>();
            return weixinIps.ip_list;
        }
        #endregion


        #region 连接ip并响应给微信
        /// <summary>
        /// 连接微信ip 并推送响应消息
        /// </summary>
        /// <param name="message">连接微信ip 并推送响应消息</param>
        public void WeiXinClientMessage(string message)
        {


        }
        #endregion

        #region 权限与帮助文档

        #region 获取用户权限组
        /// <summary>
        /// 根据用户id获取用户的权限
        /// </summary>
        /// <param name="weiXinUserId">微信用户id</param>
        /// <returns></returns>
        private WeiXinUserPowers GetPowerByWeixinUserId(string weiXinUserId)
        {
            var userPower = new WeiXinUserPowers();
            //#region 能力追加
            //userPower.ChangePayPassWord = Service.Enums.WeiXinTaskRegStr.ChangePayPassWordPower.Split(',').ToList().Contains(weiXinUserId);
            //userPower.ChangePassWord = Service.Enums.WeiXinTaskRegStr.ChangePassWordPower.Split(',').ToList().Contains(weiXinUserId);
            //userPower.SearchUserId = Service.Enums.WeiXinTaskRegStr.SearchUserIdPower.Split(',').ToList().Contains(weiXinUserId);
            //#endregion
            //userPower.IsPower = userPower.ChangePayPassWord || userPower.ChangePassWord || userPower.SearchUserId;
            return userPower;

        }
        #endregion

        #region 帮助文档
        /// <summary>
        /// 微信功能帮助文档
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        private string WeixinPowerHelpText(WeiXinUserPowers power)
        {

            #region 帮助文档列表
            var listhelptxts = new List<string>();
            #region 根据权限追加
            #endregion
            if (power.ChangePassWord)
            {
                listhelptxts.Add("如果想修改密码请回复：修改密码 4个字,系统将给你提供修改规则");
            }
            if (power.ChangePayPassWord)
            {
                listhelptxts.Add("如果想修改支付密码请回复：支付密码 4个字,系统将给你提供修改规则");
            }
            if (power.SearchUserId)
            {
                listhelptxts.Add("如果想查询用户id请回复：查询用户 4个字,系统将给你提供修改规则");
            }
            var HelpTxt = string.Empty;
            if (listhelptxts.Count == 0)
            {
                return HelpTxt;
            }
            //权限帮助文档
            for (int i = 0; i < listhelptxts.Count; i++)
            {
                HelpTxt += string.Format((i + 1) + ":" + listhelptxts[i] + ";" + "\n");
            }
            #endregion
            return HelpTxt;
        }
        #endregion
        #endregion


        #region 根据命令和权限给用户提示
        /// <summary>
        ///  根据命令和权限给用户提示
        /// </summary>
        /// <param name="Content">用户信息</param>
        /// <param name="HelpText">帮助文档</param>
        /// <param name="power">命令</param>
        /// <returns></returns>
        private string getSendContent(string Content, string HelpText, WeiXinUserPowers power)
        {
            return "";
            //if (!power.IsPower)
            //{
            //    return string.Empty;
            //}

            //#region 修改支付密码
            ////修改支付密码
            //if (power.ChangePayPassWord)
            //{
            //    if (Common.Helpers.StringHelper.StrIsMatch(Content, Service.Enums.WeiXinTaskRegStr.ChangePayPassword).Success)
            //    {
            //        return WeixinContentFillter(Content, Service.Enums.WeiXinTaskRegStr.ChangePayPassword);
            //    }
            //    else if (Content == "TX")
            //    {
            //       // WalletService.WithdrawReady();
            //        return "提现信息已发送!:)";
            //    }
            //    else if (Content == "支付密码")
            //    {
            //        return Service.Enums.WeiXinTaskRegStr.ChangePayPasswordDsec;
            //    }

            //}
            //#endregion

            //#region 修改密码
            ////修改密码
            //if (power.ChangePassWord)
            //{
            //    if (Common.Helpers.StringHelper.StrIsMatch(Content, Service.Enums.WeiXinTaskRegStr.ChangePassword).Success)
            //    {
            //        return ChangePassword(Content, Service.Enums.WeiXinTaskRegStr.ChangePassword, Service.Enums.WeiXinTaskRegStr.ChangePasswordDsec);
            //    }
            //    else if (Content == "修改密码")
            //    {
            //        return Service.Enums.WeiXinTaskRegStr.ChangePasswordDsec;
            //    }

            //}
            //#endregion

            //#region 查询用户Id
            ////查询用户ID
            //if (power.SearchUserId)
            //{
            //    if (Common.Helpers.StringHelper.StrIsMatch(Content, Service.Enums.WeiXinTaskRegStr.SearchUserId).Success)
            //    {
            //        return GetUser(Content, Service.Enums.WeiXinTaskRegStr.SearchUserId, Service.Enums.WeiXinTaskRegStr.SearchUserIdDsec);
            //    }
            //    else if (Content == "查询用户")
            //    {
            //        return Service.Enums.WeiXinTaskRegStr.SearchUserIdDsec;
            //    }
            //}
            //#endregion


            //return HelpText;
        }
        #endregion


        #region 消息命令处理

        #region 修改支付密码
        /// <summary>
        /// 消息命令处理
        /// </summary>
        /// <param name="weixincontent">微信消息</param>
        /// <param name="RegStr">微信消息</param>
        /// <returns></returns>
        public string WeixinContentFillter(string weixincontent, string RegStr)
        {

            var result = "";
            var math = Common.Helpers.StringHelper.StrIsMatch(weixincontent, RegStr);
            if (!math.Success)
            {
                return result;
            }
            if (!math.Groups[1].Success || !math.Groups[2].Success)
            {
                return result;
            }
            var userName = math.Groups[1].Value;
            var password = math.Groups[2].Value;
            var user = AccountService.GetByPhone(userName);
            if (user == null)
            {
                return "用户不存在";
            }
            //BalanceService.ResetPaymentPasswordB(userName, password);
            return string.Format("账户{0}的支付密码修改成功!", userName);
        }
        #endregion


        #region 修改用户密码命令
        /// <summary>
        /// 消息命令处理
        /// </summary>
        /// <param name="weixincontent">微信消息</param>
        /// <param name="RegStr">微信消息</param>
        /// <param name="resultTxt">处理结果消息</param>
        /// <returns></returns>
        private string ChangePassword(string weixincontent, string RegStr, string resultTxt)
        {
            var result = string.IsNullOrEmpty(resultTxt) ? "不符合命令规则" : resultTxt;
            var math = Common.Helpers.StringHelper.StrIsMatch(weixincontent, RegStr);
            if (!math.Success)
            {
                return result;
            }
            if (!math.Groups[1].Success || !math.Groups[3].Success)
            {
                return result;
            }
            var userName = math.Groups[1].Value;
            var password = math.Groups[3].Value;
            var user = AccountService.GetByPhone(userName);
            if (user == null)
            {
                return "用户不存在";
            }
            AccountService.ChangePasswordB(user.UserName, password);
            return string.Format("账户{0}的密码修改成功!", userName);
        }
        #endregion


        #region 根据用户输入的手机号查询并返回用户ID命令
        /// <summary>
        /// 消息命令处理
        /// </summary>
        /// <param name="weixincontent">微信消息</param>
        /// <param name="RegStr">微信消息</param>
        /// <param name="resultTxt">处理结果消息</param>
        /// <returns></returns>
        private string GetUser(string weixincontent, string RegStr, string resultTxt)
        {
            var result = string.IsNullOrEmpty(resultTxt) ? "不符合命令规则" : resultTxt;
            var math = Common.Helpers.StringHelper.StrIsMatch(weixincontent, RegStr);
            if (!math.Success)
            {
                return result;
            }
            if (!math.Groups[1].Success)
            {
                return result;
            }
            var userName = math.Groups[1].Value;
            var user = AccountService.GetByPhone(userName);
            if (user == null)
            {
                return "对不起,你搜索的用户不存在!";
            }
            return string.Format("您搜索的账户{0}的用户Id为:{1}", user.UserName, user.UserId);
        }

        #endregion

        #endregion


        #region xml解析
        #region
        /// <summary>
        /// 加载信息
        /// </summary>
        /// <param name="sMsg"></param>
        public void LoadXml(string sMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sMsg);
            XmlNode cropid = doc.SelectSingleNode("//ToUserName");
            string cropId = cropid.InnerText;//获取企业id
            XmlNode from = doc.SelectSingleNode("//FromUserName");
            string userid = from.InnerText;//获取员工id

            XmlNode type = doc.SelectSingleNode("//MsgType");
            string msgtype = type.InnerText;//消息的类型

            XmlNode Event = doc.SelectSingleNode("//Event");
            string Events = Event.InnerText;//事件的类型

            XmlNode AgentID = doc.SelectSingleNode("//AgentID");
            string agentid = AgentID.InnerText;//应用id

            XmlNode EventKey = doc.SelectSingleNode("//EventKey");
            string eventkey = EventKey.InnerText;//出发事件的唯一标示键，极为重要

        }
        #endregion
        /// <summary>
        /// 获取post过来包中的xml数据
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public string GetXml(Stream inputStream)
        {
            var strLen = Convert.ToInt32(inputStream.Length);
            var strArr = new byte[strLen];
            inputStream.Read(strArr, 0, strLen);
            var requestMes = Encoding.UTF8.GetString(strArr);
            inputStream.Flush();
            inputStream.Close();
            inputStream.Dispose();
            return requestMes;
        }
        #endregion


        #region 事件监听
        /// <summary>
        /// 点击了查询员工事件
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="fromuser"></param>
        /// <param name="createtime"></param>
        /// <returns></returns>
        public string SelectUserInfo(string touser, string fromuser, string createtime)
        {
            string content = "输入您要查询的：\r\n  员工姓名或者员工编号";
            string str = string.Format(@"<xml>
                                        <ToUserName><![CDATA[{0}]]></ToUserName>
                                        <FromUserName><![CDATA[{1}]]></FromUserName> 
                                        <CreateTime>{2}</CreateTime>
                                        <MsgType><![CDATA[text]]></MsgType>
                                        <Content><![CDATA[{3}]]></Content>
                                      </xml>", touser, fromuser, createtime, content);
            return str;
        }


        /// <summary>
        /// 点击了设置数据库事件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="touser"></param>
        /// <param name="fromuser"></param>
        /// <param name="createtime"></param>
        /// <returns></returns>
        public string SetData(string content, string touser, string fromuser, string createtime)
        {
            string str = string.Format(@"<xml>
                                        <ToUserName><![CDATA[{0}]]></ToUserName>
                                        <FromUserName><![CDATA[{1}]]></FromUserName> 
                                        <CreateTime>{2}</CreateTime>
                                        <MsgType><![CDATA[text]]></MsgType>
                                        <Content><![CDATA[{3}]]></Content>
                                      </xml>", touser, fromuser, createtime, content);
            return str;
        }

        #endregion


        #region 无意义事件提醒
        /// <summary>
        /// 用户无意义操作
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="fromuser"></param>
        /// <param name="createtime"></param>
        /// <returns></returns>
        public string NothingDo(string touser, string fromuser, string createtime)
        {
            string content = "呃…不大明白，或者您的问题真的难倒我了！您也可以输入序号快速办理业务：\r\n[1]通信录查询\r\n[2]员工信息查询\r\n[3]我的基本信息\r\n...\r\n还无法解决您的问题吗？那就联系管理员吧！";
            string str = string.Format(@"<xml>
                                        <ToUserName><![CDATA[{0}]]></ToUserName>
                                        <FromUserName><![CDATA[{1}]]></FromUserName> 
                                        <CreateTime>{2}</CreateTime>
                                        <MsgType><![CDATA[text]]></MsgType>
                                        <Content><![CDATA[{3}]]></Content>
                                      </xml>", touser, fromuser, createtime, content);
            return str;

        }

        #endregion

        #endregion
        #endregion
        #endregion


        #region 公众号
        #region 微信公众号认证

        #region 第一步code 换取tokken和openid
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WeiXinAuthTokkenModel GetOpenWeiXinAuthToken(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                //重新获取code
            }
            var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", OPENCORPID, OPENCORPSECRET, code);
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result);
            var status = JsonResult.ToObject<WeiXinStatus>();
            if (!string.IsNullOrEmpty(status.ErrMsg))
            {
                return null;
                //重新获取code
            }
            var jsontokken = JsonResult.ToObject<WeiXinAuthTokkenModel>();
            return jsontokken;
        }
        #endregion

        #region 公众号token
        #region 获取公众号token
        /// <summary>
        /// 获取微信tokken
        /// </summary>
        /// <returns></returns>
        private string GetOpenTokken()
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", OPENCORPID, OPENCORPSECRET);
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result);
            var status = JsonResult.ToObject<WeiXinStatus>();
            var jsontokken = JsonResult.ToObject<WeiXinAccessToken>();
            return jsontokken.Access_Token;
        }
        #endregion

        #region 检测tokken
        /// <summary>
        /// 检测token合法性
        /// </summary>
        /// <param name="openid">公众号用户id</param>
        /// <param name="access_token">token</param>
        /// <returns></returns>
        public bool checkToken(string openid, string access_token)
        {
            return false;
        }
        #endregion

        #region 获取缓存Tokken
        /// <summary>
        /// 获取缓存Tokken
        /// </summary>
        /// <returns></returns>
        public string GetOpenCaCheTokken()
        {
            var cacheval = HttpRuntime.Cache["WeiXinOpenTokken"];
            if (cacheval == null)
            {
                var result = GetOpenTokken();
                HttpRuntime.Cache.Insert("WeiXinOpenTokken", result, null, DateTime.Now.AddSeconds(5400), System.Web.Caching.Cache.NoSlidingExpiration);
                return result;
            }
            return (string)HttpRuntime.Cache["WeiXinOpenTokken"];
        }
        #endregion
        #endregion


        #region 获取用户详细信息
        /// <summary>
        /// 获取公众号用户详情
        /// </summary>
        /// <param name="openId"></param>

        private WeiXinOpenUserViewModel GetOpenUser(string openId)
        {
            var token = GetOpenCaCheTokken();

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", token, openId);
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            var JsonResult = JObject.Parse(result);
            var status = JsonResult.ToObject<WeiXinStatus>();
            if (!string.IsNullOrEmpty(status.ErrMsg))
            {
                GetOpenTokken();
            }
            var weixinopenUser = JsonResult.ToObject<WeiXinOpenUserViewModel>();
            return weixinopenUser;
        }
        #endregion


        #endregion
        #endregion


    }
}
