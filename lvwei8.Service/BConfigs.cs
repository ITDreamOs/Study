using lvwei8.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service
{
    public class BConfigs
    {

        /// <summary>
        /// 套餐推送券卡的默认有效期设置
        /// </summary>
        public static int PackageCouponPeriodOfValidity = 30;
        /// <summary>
        /// 调试模式
        /// </summary>
        public static bool DebugMode = ConfigHelper.GetAppSettingBoolean("debugMode", false);

        /// <summary>
        /// 是否启用web api压缩
        /// </summary>
        public static bool EnableWebAPICompression = ConfigHelper.GetAppSettingBoolean("EnableWebAPICompression", false);

        /// <summary>
        /// BaiduAkForServer
        /// </summary>
        public static string BaiduAkForServer = ConfigHelper.GetAppSetting("baiduAkForServer");

        /// <summary>
        /// FileHttpRoot
        /// </summary>
        public static string FileHttpRoot = ConfigHelper.GetAppSetting("FileHttpRoot");



        /// <summary>
        /// FileHttpRootReal  附件网络地址，真实地址,不能为空
        /// </summary>
        public static string FileHttpRoot_Real = ConfigHelper.GetAppSetting("FileHttpRootReal");

        /// <summary>
        /// FileRoot
        /// </summary>

        public static string FileRoot = ConfigHelper.GetAppSetting("FileRoot");


        /// <summary>
        /// APP日志文件保存的位置
        /// </summary>
        public static string AppLogLocation = ConfigHelper.GetAppSetting("AppLogLocation");

        /// <summary>
        /// 自动好评：订单完成之后，N天自动好评
        /// </summary>
        public static int AutoGoodReview = ConfigHelper.GetAppSettingInt("AutoGoodReview", 15);
        /// <summary>
        /// 未支付订单，自动取消订单
        /// </summary>
        public static int AutoCancelOrder = ConfigHelper.GetAppSettingInt("AutoCancelOrder", 1);
        /// <summary>
        /// 标签帮助说明
        /// </summary>
        public static string LabelHelpUrl = ConfigHelper.GetAppSetting("LabelHelpUrl");




        /// <summary>
        /// 后台地址
        /// </summary>
        public static string MvcBankendUrl = ConfigHelper.GetAppSetting("MvcBankendUrl");

        /// <summary>
        /// 用于系统消息的系统名称
        /// </summary>
        public const string SystemNameForSystemMsg = "驴尾巴";


        public static Dictionary<T, string> GetEnumDescriptions<T>()
        {
            var descs = new Dictionary<T, string>();
            var values = Enum.GetValues(typeof(T));
            foreach (var value in values)
            {
                var enumValue = (T)value;
                string desc = GetEnumDesc<T>(enumValue);
                descs.Add(enumValue, desc);
            }
            return descs;
        }

        public static string GetEnumDesc<T>(T enumValue)
        {
            if (enumValue == null) return null;
            var field = typeof(T).GetField(enumValue.ToString());
            if (field == null)
                return string.Empty;
            var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
            string desc = enumValue.ToString();
            if (fds != null) desc = ((DescriptionAttribute)fds).Description;
            return desc;
        }

        /// <summary>
        /// 申请状态
        /// </summary>
        public static Dictionary<string, string> ApplyStauts
        {
            get
            {
                var payStauts = new Dictionary<string, string>();
                payStauts.Add("等待审核", "等待审核");
                payStauts.Add("审核通过", "审核通过");
                payStauts.Add("拒绝", "拒绝");
                return payStauts;
            }
        }

        /// <summary>
        /// Api授权用户
        /// </summary>
        public static Dictionary<int, string> ApiClientStore
        {
            get
            {
                var apiClientStore = new Dictionary<int, string>();
                apiClientStore.Add((int)ApiClient.Android, "d0ad69e32f454e20a9e1589d8e3a4147");
                apiClientStore.Add((int)ApiClient.IOS, "187b3eb5afb0405c8c2aeacd6347ab26");
                apiClientStore.Add((int)ApiClient.Web, "31ec2ffa68bc4ce7ae09b05c03661c91");
                return apiClientStore;
            }
        }




        public static Dictionary<int, string> AnnouncementType
        {
            get
            {
                var announcementType = new Dictionary<int, string>();
                announcementType.Add(1, "总站公告");
                return announcementType;
            }
        }





        /// <summary>
        /// 支付宝E修账户私钥
        /// </summary>
        public static string AlipayPrivate_key = ConfigHelper.GetAppSetting("AlipayPrivate_key");
        public static string AlipayPublic_key = ConfigHelper.GetAppSetting("AlipayPublic_key");
        public static string AlipayNotifyUrl = ConfigHelper.GetAppSetting("AlipayNotifyUrl");
        public static string AlipayCallbackUrl = ConfigHelper.GetAppSetting("AlipayCallbackUrl");
        public static string AlipayCancelUrl = ConfigHelper.GetAppSetting("AlipayCancelUrl");
        public static string AlipayPartner = ConfigHelper.GetAppSetting("AlipayPartner");
        public static string AlipaySeller_email = ConfigHelper.GetAppSetting("AlipaySeller_email");

        /// <summary>
        /// 支付宝E修账户私钥
        /// </summary>
        public static string AlipayInput_charset = ConfigHelper.GetAppSetting("AlipayInput_charset");
        /// <summary>
        /// WebApi授权认证键名
        /// </summary>
        public static string ApiTokenName = ConfigHelper.GetAppSetting("ApiTokenName");
        /// <summary>
        /// 是否开启接口授权验证
        /// </summary>
        public static bool ApiAuthenticationEnabled = ConfigHelper.GetAppSettingBoolean("ApiAuthenticationEnabled", false);
        /// <summary>
        /// Token失效时间(单位分钟)
        /// </summary>
        public static int ApiTokenExpire = ConfigHelper.GetAppSettingInt("ApiTokenExpire", -999999);
        /// <summary>
        /// 万能验证码
        /// </summary>
        public static string GodKey = ConfigHelper.GetAppSetting("GodKey");

        /// <summary>
        /// 模拟发送验证码
        /// </summary>
        public static bool SimulateSendVCode = ConfigHelper.GetAppSettingBoolean("simulateSendVCode", false);

        /// <summary>
        /// 检查频率(以秒为单位)
        /// </summary>
        public static int Expert_CheckInterval = ConfigHelper.GetAppSettingInt("expert_CheckInterval", 60);

        /// <summary>
        /// 上传触发的最小距离(以米为单位)
        /// </summary>
        public static int Expert_UploadMinDistance = ConfigHelper.GetAppSettingInt("expert_UploadMinDistance", 1000);




        /// <summary>
        /// 清算日
        /// </summary>
        public static int ClearDay = ConfigHelper.GetAppSettingInt("ClearDay", 1);
        /// <summary>
        /// 充值的钱的结算周期
        /// </summary>
        public static int RechargeClearDay = ConfigHelper.GetAppSettingInt("RechargeClearDay", 180);
        /// <summary>
        /// 银行手续费
        /// </summary>
        public static Decimal BankChargesCoefficient = ConfigHelper.GetAppSettingDecimal("BankChargesCoefficient", 0.007m);

        public static List<int> RescueLabelIds
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("RescueLabelIds");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }


        /// <summary>
        /// 是否启用授权服务
        /// </summary>
        public static bool EnableAuthorization = ConfigHelper.GetAppSettingBoolean("EnableAuthorization", false);
        /// <summary>
        /// 启用图形验证码校验
        /// </summary>
        public static bool EnableCaptchaCheck = ConfigHelper.GetAppSettingBoolean("EnableCaptchaCheck", false);

        /// <summary>
        /// 启用图形验证码校验
        /// </summary>
        public static bool SendEmail = ConfigHelper.GetAppSettingBoolean("SendEmail", false);
    }
}
