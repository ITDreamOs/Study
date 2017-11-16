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
        /// BaseScoreWeight(基础评分权重)
        /// </summary>
        public static int BaseScoreWeight = ConfigHelper.GetAppSettingInt("BaseScoreWeight", -999999);
        /// <summary>
        /// 所有品牌
        /// </summary>
        public static string CarCodeAll = ConfigHelper.GetAppSetting("CarCodeAll");
        /// <summary>
        /// APP日志文件保存的位置
        /// </summary>
        public static string AppLogLocation = ConfigHelper.GetAppSetting("AppLogLocation");
        /// <summary>
        /// IOS是否审核通过
        /// </summary>
        public static string IsIosApproved = ConfigHelper.GetAppSetting("IsIosApproved_AppStore", "0|0|0|0");
        /// <summary>
        /// 地球上两点之间的最大最短距离界限值
        /// </summary>
        public const int MaxDistance = 99999999;
        /// <summary>
        /// 允许收钱方取消订单的间隔时间
        /// </summary>
        public static int CancelOrderIntervalTime = ConfigHelper.GetAppSettingInt("CancelOrderIntervalTime", 15);
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

        #region 认证推送（审核员）
        /// <summary>
        /// 微信企业号信息发送对象
        /// </summary>
        public static string WeiXinQiYeHaoSendToUser = ConfigHelper.GetAppSetting("WeiXinQiYeHaoSendToUser");

        /// <summary>
        /// 微信企业号信息发送分组
        /// </summary>
        public static string WeiXinQiYeHaoSendToParty = ConfigHelper.GetAppSetting("WeiXinQiYeHaoSendToParty");
        /// <summary>
        /// 券卡最多使用张数对应的限制天数：原来是单个订单，现改为天
        /// </summary>
        public static int CouponMaxConsumLimitedDays = ConfigHelper.GetAppSettingInt("CouponMaxConsumLimitedDays", 1);
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public static int DaysOfYear = 365;



        #region 微信财务推送
        /// <summary>
        /// 微信企业号信息发送对象
        /// </summary>
        public static string WeiXinQiYeHaoSendToUserForWithdraw = ConfigHelper.GetAppSetting("WeiXinQiYeHaoSendToUserForWithdraw");

        /// <summary>
        /// 微信企业号信息发送分组
        /// </summary>
        public static string WeiXinQiYeHaoSendToPartyForWithdraw = ConfigHelper.GetAppSetting("WeiXinQiYeHaoSendToPartyForWithdraw");
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public static string MvcBankendUrl = ConfigHelper.GetAppSetting("MvcBankendUrl");

        /// <summary>
        /// 用于系统消息的系统名称
        /// </summary>
        public const string SystemNameForSystemMsg = "驴尾巴";
        /// <summary>
        /// 邀请商家时短信内容
        /// </summary>
        public static string ShareMyPromotionSMSContent = ConfigHelper.GetAppSetting("ShareMyPromotionSMSContent");
        /// <summary>
        /// 用户推广二维码连接地址
        /// </summary>
        public static string UserQRUrl = ConfigHelper.GetAppSetting("UserQRUrl");

        public static int IsOpenBugTags = ConfigHelper.GetAppSettingInt("IsOpenBugTags", 1);
        /// <summary>
        /// 推广内容
        /// </summary>
        public static string PopularizeConent = ConfigHelper.GetAppSetting("PopularizeConent", "");

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
        ///// <summary>
        ///// 总站支付类型
        ///// </summary>
        //public static Dictionary<sbyte, string> StationPayType
        //{
        //    get
        //    {
        //        var payType = new Dictionary<sbyte, string>();
        //        payType.Add((sbyte)MasterStationPayType.JoinAlli, "加盟保证金");
        //        payType.Add((sbyte)MasterStationPayType.Service, "发行优惠券");
        //        payType.Add((sbyte)MasterStationPayType.Spread, "平台推广");
        //        payType.Add((sbyte)MasterStationPayType.AcrService, "发行券");
        //        payType.Add((sbyte)MasterStationPayType.Deposit, "充值");
        //        payType.Add((sbyte)MasterStationPayType.Withdraw_For_NewCarOwner, "推送券");
        //        payType.Add((sbyte)MasterStationPayType.Withdraw_For_OldContacter, "推送券");
        //        payType.Add((sbyte)MasterStationPayType.Withdraw_PreferentialCoupon, "购买特惠券");
        //        return payType;
        //    }
        //}
        //public static Dictionary<sbyte, string> StationPayStauts
        //{
        //    get
        //    {
        //        var payStauts = new Dictionary<sbyte, string>();
        //        payStauts.Add((sbyte)StationPayStatus.Waiting_Pay, "待支付");
        //        payStauts.Add((sbyte)StationPayStatus.Complete_Order, "已完成");
        //        payStauts.Add((sbyte)StationPayStatus.Canceled_Order, "已取消");
        //        payStauts.Add((sbyte)StationPayStatus.Complete_Pay, "支付待确认");
        //        return payStauts;
        //    }
        //}
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
        //public static Dictionary<int, string> OrderStauts
        //{
        //    get
        //    {
        //        var payStauts = new Dictionary<int, string>();
        //        payStauts.Add((int)OrderStatus.Waiting_Buyer_ToPay, "待付款");
        //        payStauts.Add((int)OrderStatus.Buyer_Complete_Pay, "待确认");
        //        payStauts.Add((int)OrderStatus.Buyer_Canceled_Order, "已取消");
        //        payStauts.Add((int)OrderStatus.Seller_Complete_Order, "已完成");
        //        payStauts.Add((int)OrderStatus.Seller_Rejected_Order, "已拒绝");
        //        return payStauts;
        //    }
        //}
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
        ///// <summary>
        ///// 支付方式
        ///// </summary>
        //public static Dictionary<int, string> PaymentTypes
        //{
        //    get
        //    {
        //        var payStauts = new Dictionary<int, string>();
        //        payStauts.Add((int)PaymentModeType.Alipay, "支付宝");
        //        payStauts.Add((int)PaymentModeType.Cash, "现金支付");
        //        return payStauts;
        //    }
        //}
        /// <summary>
        /// 专家擅长领域
        /// </summary>
        public static Dictionary<string, string> ExpertSkills
        {
            get
            {
                var expertSkills = new Dictionary<string, string>();
                expertSkills.Add("常规保养", "常规保养");
                expertSkills.Add("机修", "机修");
                expertSkills.Add("电路", "电路");
                expertSkills.Add("变速箱", "变速箱");
                expertSkills.Add("钣金喷漆", "钣金喷漆");
                expertSkills.Add("汽车空调", "汽车空调");
                expertSkills.Add("四轮定位", "四轮定位");
                expertSkills.Add("汽车改装", "汽车改装");
                expertSkills.Add("修配钥匙", "修配钥匙");
                return expertSkills;
            }
        }

        public static Dictionary<string, string> ExpertTechGrades
        {
            get
            {
                var expertTechGrades = new Dictionary<string, string>();
                expertTechGrades.Add("初级工", "初级工");
                expertTechGrades.Add("中级工", "中级工");
                expertTechGrades.Add("高级工", "高级工");
                expertTechGrades.Add("技师", "技师");
                expertTechGrades.Add("高级技师", "高级技师");
                return expertTechGrades;
            }
        }

        //public static Dictionary<int, string> OpenFireMsgTypeCodes
        //{
        //    get
        //    {
        //        var openFireMsgTypeCodes = new Dictionary<int, string>();
        //        openFireMsgTypeCodes.Add((int)MsgType.OrderRemind, "00009");
        //        //openFireMsgTypeCodes.Add((int)MsgType.MoveCar, "00011");
        //        return openFireMsgTypeCodes;
        //    }
        //}


        public static Dictionary<int, string> AnnouncementType
        {
            get
            {
                var announcementType = new Dictionary<int, string>();
                announcementType.Add(1, "总站公告");
                return announcementType;
            }
        }

        public static List<int> CarPoolSchedule
        {
            get
            {
                var carPoolSchedule = new List<int>();
                carPoolSchedule.Add(7);
                carPoolSchedule.Add(9);
                carPoolSchedule.Add(11);
                carPoolSchedule.Add(13);
                carPoolSchedule.Add(15);
                carPoolSchedule.Add(17);
                return carPoolSchedule;
            }
        }

        #region 配件商
        /// <summary>
        /// 配件商产品类别跟类别编码
        /// </summary>
        public static int AcrProductCategoryRootCode = ConfigHelper.GetAppSettingInt("AcrProductCategoryRootCode", 3000000);

        /// <summary>
        /// 关联车辆品牌
        /// </summary>
        public static List<int> AutoParts_RelCarBrand
        {
            get
            {
                var cfg = ConfigHelper.GetAppSetting("AutoParts_RelCarBrand");
                return cfg.Split(',').Select(e => int.Parse(e)).ToList();
            }
        }

        /// <summary>
        /// 关联商品类别
        /// </summary>
        public static List<int> AutoParts_RelPCategory
        {
            get
            {
                var cfg = ConfigHelper.GetAppSetting("AutoParts_RelPCategory");
                return cfg.Split(',').Select(e => int.Parse(e)).ToList();
            }
        }
        /// <summary>
        /// 在日志中记录密码
        /// </summary>
        public static bool EnableLogPwd = ConfigHelper.GetAppSettingBoolean("EnableLogPwd", false);

        /// <summary>
        /// 是否将特殊收费给配件商，默认为True
        /// </summary>
        public static bool SpecialToAcr = ConfigHelper.GetAppSettingBoolean("SpecialToAcr", true);

        /// <summary>
        /// 是否允许和自己交易
        /// </summary>
        public static bool AllowSelfTrade = ConfigHelper.GetAppSettingBoolean("AllowSelfTrade", false);

        #endregion

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
        /// 老客户服务费率
        /// </summary>
        public static int ServiceRate = ConfigHelper.GetAppSettingInt("ServiceRate", 999999);
        /// <summary>
        /// 新客户服务费率
        /// </summary>
        public static int ServiceRateForNew = ConfigHelper.GetAppSettingInt("ServiceRateForNew", 999999);
        /// <summary>
        /// 平台服务费率
        /// </summary>
        public static int SpreadRate = ConfigHelper.GetAppSettingInt("SpreadRate", 999999);
        /// <summary>
        /// 是否启用发送挪车短信通知
        /// </summary>
        public static bool EnableMoveCarSmsNotify = ConfigHelper.GetAppSettingBoolean("EnableMoveCarSmsNotify", false);
        /// <summary>
        /// 获取46655 AppKey
        /// </summary>
        public static string AppKey46644 = ConfigHelper.GetAppSetting("AppKey46644");
        /// <summary>
        /// 获取Haoservice AppKey
        /// </summary>
        public static string AppKeyHaoservice = ConfigHelper.GetAppSetting("AppKeyHaoservice");
        /// <summary>
        /// 获取获取违章信息查询通道
        /// </summary>
        public static string TrafficViolationApiChannel = ConfigHelper.GetAppSetting("TrafficViolationApiChannel");
        /// <summary>
        /// 获取获取违章信息查询通道(调试状态),
        /// </summary>
        public static bool TrafficViolationApiChannel_Debug = (ConfigHelper.GetAppSetting("TrafficViolationApiChannel_Debug") == "1");
        /// <summary>
        /// 获取聚合云数据平台违章查询OpenID
        /// </summary>
        public static string AppKeyJuHe = ConfigHelper.GetAppSetting("AppKeyJuHe");
        /// <summary>
        /// 获取车首页违章查询AppKey
        /// </summary>
        public static string AppKeyCheShouYe = ConfigHelper.GetAppSetting("AppKeyCheShouYe");
        /// <summary>
        /// 获取车首页违章查询AppID
        /// </summary>
        public static string AppIdCheShouYe = ConfigHelper.GetAppSetting("AppIdCheShouYe");
        // 缩略小图宽
        public static int ThumbnailSize_LW = ConfigHelper.GetAppSettingInt("ThumbnailSize_LW", 250);
        // 缩略小图高
        public static int ThumbnailSize_LH = ConfigHelper.GetAppSettingInt("ThumbnailSize_LH", 150);
        // 缩略中图宽
        public static int ThumbnailSize_MW = ConfigHelper.GetAppSettingInt("ThumbnailSize_MW", 440);
        // 缩略中图高
        public static int ThumbnailSize_MH = ConfigHelper.GetAppSettingInt("ThumbnailSize_MH", 264);
        // 头像缩略小图宽
        public static int HeadSize_LW = ConfigHelper.GetAppSettingInt("HeadSize_LW", 100);
        // 头像缩略小图高
        public static int HeadSize_LH = ConfigHelper.GetAppSettingInt("HeadSize_LH", 100);

        /// <summary>
        /// 拼车可以发布的日期范围
        /// </summary>
        public static int CarPoolDateRange = ConfigHelper.GetAppSettingInt("CarPoolDateRange", 3);

        /// <summary>
        /// 租车交车地点匹配时的最大距离, 单位米。 如果超出该值，则不匹配。
        /// </summary>
        public static double RentalCarMatchMaxDistance = ConfigHelper.GetAppSettingInt("RentalCarMatchMaxDistance", 1) * 1000;

        /// <summary>
        /// 租车交还车时间匹配范围, 单位分钟
        /// </summary>
        public static int RentalCarMatchDateRange = ConfigHelper.GetAppSettingInt("RentalCarMatchDateRange", 60);

        /// <summary>
        /// 租车保险价格
        /// </summary>
        public static decimal RentalCarInsurancePrice = ConfigHelper.GetAppSettingDecimal("RentalCarInsurancePrice");

        /// <summary>
        /// 不计免赔险价格
        /// </summary>
        public static decimal RentalCarExcludingInsurancePrice = ConfigHelper.GetAppSettingDecimal("RentalCarExcludingInsurancePrice");

        /// <summary>
        /// 租车押金金额
        /// </summary>
        public static decimal RentalCarDepositAmount = ConfigHelper.GetAppSettingDecimal("RentalCarDepositAmount");

        /// <summary>
        /// 启用二三级的job处理
        /// </summary>
        public static bool EnableLv2Lv3Job = ConfigHelper.GetAppSettingBoolean("EnableLv2Lv3Job", false);

        #region 天翼短信配置

        #region e修联盟应用

        /// <summary>
        /// 天翼appid
        /// </summary>
        public static string tyAppid = ConfigHelper.GetAppSetting("tyAppid");
        /// <summary>
        /// 天翼AppSecret
        /// </summary>
        public static string tyAppSecret = ConfigHelper.GetAppSetting("tyAppSecret");

        #endregion

        #region 锦衣卫

        /// <summary>
        /// 天翼appid
        /// </summary>
        public static string jyw_TyAppid = ConfigHelper.GetAppSetting("jyw_TyAppid");
        /// <summary>
        /// 天翼AppSecret
        /// </summary>
        public static string jyw_TyAppSecret = ConfigHelper.GetAppSetting("jyw_TyAppSecret");

        #endregion

        #endregion

        /// <summary>
        /// 推广地址
        /// </summary>
        public static string SharePromotionUrl = ConfigHelper.GetAppSetting("SharePromotionUrl");

        /// <summary>
        /// 基地址
        /// </summary>
        public static string BaseUrl = ConfigHelper.GetAppSetting("BaseUrl", "");

        /// <summary>
        /// 邀请短信模板
        /// </summary>
        public static string InviteSmsTemplate = ConfigHelper.GetAppSetting("InviteSmsTemplate", "");

        /// <summary>
        /// 重复商家距离判断范围
        /// </summary>
        public static int RepeatJudgeDistance = ConfigHelper.GetAppSettingInt("RepeatJudgeDistance", 500);

        /// <summary>
        /// 平台佣金系数
        /// </summary>
        public static double P_CommissionCoefficient = ConfigHelper.GetAppSettingDouble("P_CommissionCoefficient", 0.01);
        /// <summary>
        /// 上线佣金系数
        /// </summary>
        public static double Up_CommissionCoefficient = ConfigHelper.GetAppSettingDouble("Up_CommissionCoefficient", 0.01);
        /// <summary>
        /// 是否开启接口HTTPS验证
        /// </summary>
        public static bool UriHttpsCheck = ConfigHelper.GetAppSettingBoolean("UriHttpsCheck", false);
        /// <summary>
        /// 启用短信提供者轮询
        /// </summary>
        public static bool EnableSmsProviderPooling = ConfigHelper.GetAppSettingBoolean("EnableSmsProviderPooling", false);
        /// <summary>
        /// 一天内最大发送次数
        /// </summary>
        public static int OneDayMaxToSendActivateTimes = ConfigHelper.GetAppSettingInt("OneDayMaxToSendActivateTimes", 3);
        /// <summary>
        /// 激活过期时间
        /// </summary>
        public static int ActivateExprieDays = ConfigHelper.GetAppSettingInt("ActivateExprieDays", 3);

        public static List<string> AccessControlAllowOrigin = ConfigHelper.GetAppSettingList("Access-Control-Allow-Origin");
        public static string AccessControlAllowHeaders = ConfigHelper.GetAppSetting("Access-Control-Allow-Headers");
        public static string AccessControlAllowMethods = ConfigHelper.GetAppSetting("Access-Control-Allow-Methods");
        /// <summary>
        /// 救援商家标签
        /// </summary>
        public static int RescueStoreLabelId = ConfigHelper.GetAppSettingInt("RescueStoreLabelId", -1);
        /// <summary>
        /// 晨报地址
        /// </summary>
        public static string MorningPagerUrl = ConfigHelper.GetAppSetting("MorningPagerUrl", "");
        /// <summary>
        /// 红包地址
        /// </summary>
        public static string LuckyMoneyUrl = ConfigHelper.GetAppSetting("LuckyMoneyUrl", "");

        /// <summary>
        /// 启用HangFireServer
        /// </summary>
        public static bool UseHangfireServer = ConfigHelper.GetAppSettingBoolean("UseHangfireServer", false);

        /// <summary>
        /// CacheRegisServers
        /// </summary>
        public static string CacheRegisServers = ConfigHelper.GetAppSetting("CacheRegisServers", "");
        /// <summary>
        /// 限制购买特价商品的天数
        /// </summary>
        public static int LimitPurchasePromotionDays = ConfigHelper.GetAppSettingInt("LimitPurchasePromotionDays", 30);
        /// <summary>
        /// 联盟标签图片
        /// </summary>
        public static string StoreAlliLabelPic = ConfigHelper.GetAppSetting("StoreAlliLabelPic", string.Empty);
        /// <summary>
        /// 红包过期条数
        /// </summary>
        public static int LuckyMoneyExpiredDays = ConfigHelper.GetAppSettingInt("LuckyMoneyExpiredDays", 1);
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
        /// <summary>
        /// 保险商业险返现比例
        /// </summary>
        public static int Insurance_Commercial_Rebate_Proportion = ConfigHelper.GetAppSettingInt("Insurance_Commercial_Rebate_Proportion", 10);
        /// <summary>
        /// 保险商业险返现比例
        /// </summary>
        public static int Insurance_Compulsory_Rebate_Proportion = ConfigHelper.GetAppSettingInt("Insurance_Compulsory_Rebate_Proportion", 10);
        /// <summary>
        /// 每次返现金额
        /// </summary>
        public static decimal Cashback_Per_Amount = ConfigHelper.GetAppSettingDecimal("Cashback_Per_Amount", 1m);
        /// <summary>
        /// 分红结算时间
        /// </summary>
        public static int BonusClearDay = ConfigHelper.GetAppSettingInt("BonusClearDay", 7);
        public const string WeiXinNotifyReceiver = "lizf@114995.com|weijr@114995.com|liurc@114995.com|qianjing@114995.com|jiaxy@114995.com";
        /// <summary>
        /// 嘟嘟电话每月价格
        /// </summary>
        public static decimal DuduMonthPrice = ConfigHelper.GetAppSettingDecimal("DuduMonthPrice", 0.01m);

        /// <summary>
        /// 保险标签
        /// </summary>
        public static int InsuraceLabel = ConfigHelper.GetAppSettingInt("InsuraceLabel", -1);

        // 保险洗车券
        public static int InsuranceWashCouponId = ConfigHelper.GetAppSettingInt("InsuranceWashCouponId", -1);
        // 保险保养券
        public static List<int> InsuranceMaintenanceCouponIds
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("InsuranceMaintenanceCouponIds");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }
        /// <summary>
        /// 划痕券id
        /// </summary>
        public static int InsuranceScratchRiskCouponId = ConfigHelper.GetAppSettingInt("InsuranceScratchRiskCouponId", -1);
        //智能车联标签ID
        public static int OBDWashMaintenanceLablelId = ConfigHelper.GetAppSettingInt("OBDWashMaintenanceLablelId", -1);
        public static int WashServiceLablelId = ConfigHelper.GetAppSettingInt("WashServiceLablelId", -1);
        public static int MaintenanceServiceLablelId = ConfigHelper.GetAppSettingInt("MaintenanceServiceLablelId", -1);
        public static int ScratchRiskLableId = ConfigHelper.GetAppSettingInt("ScratchRiskLableId", -1);
        // OBD洗车券
        public static int OBDWashCouponId = ConfigHelper.GetAppSettingInt("OBDWashCouponId", 732);
        // OBD保养数据
        public static int OBDMaintenanceCouponId = ConfigHelper.GetAppSettingInt("OBDMaintenanceCouponId", 733);
        // 年检券
        public static int AnnualInspectionCouponId = ConfigHelper.GetAppSettingInt("AnnualInspectionCouponId", 801);
        public static List<int> RescueLabelIds
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("RescueLabelIds");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }
        public static int FateStartValue = ConfigHelper.GetAppSettingInt("FateStartValue", 0);
        /// <summary>
        /// COBD下载地址
        /// </summary>
        public static string CobdUrl = ConfigHelper.GetAppSetting("CobdUrl");
        /// <summary>
        /// 智信通下载地址
        /// </summary>
        public static string ZxtUrl = ConfigHelper.GetAppSetting("ZxtUrl");
        /// <summary>
        /// 是否启用假评论
        /// </summary>
        public static bool enableFakeReview = ConfigHelper.GetAppSettingBoolean("enableFakeReview", false);
        /// <summary>
        /// 假一赔十标签
        /// </summary>
        public static int fakeALoseTenStoreLabelId = ConfigHelper.GetAppSettingInt("FakeALoseTenStoreLabelId", 0);
        /// <summary>
        /// 建行激活送券
        /// </summary>
        public static List<int> CBC_CouponIds
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("CBC_CouponIds");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }
        /// <summary>
        /// 建行激活送券数量
        /// </summary>
        public static List<int> CBC_CouponCounts
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("CBC_CouponCounts");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }
        /// <summary>
        /// 注册激活送券数量
        /// </summary>
        public static List<int> Reg_CouponCounts
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("Reg_CouponCounts");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }
        /// <summary>
        /// 办卡补贴，洗车券交易前10笔每笔返5元
        /// </summary>
        public static int CouponSubsidyLabelId = ConfigHelper.GetAppSettingInt("CouponSubsidyLabelId", 47);
        /// <summary>
        /// 办卡担保
        /// </summary>
        public static int CardAssureStoreLabelId = ConfigHelper.GetAppSettingInt("CardAssureStoreLabelId");

 
        /// <summary>
        /// IM记录处理完成目录
        /// </summary>
        public static string ImHistoryCompleteDir = ConfigHelper.GetAppSetting("ImHistoryCompleteDir");
        internal static decimal FreeAmountDaySameBuySell = ConfigHelper.GetAppSettingDecimal("FreeAmountDaySameBuySell", 0.01m);
        /// <summary>
        /// 白金会员卡送券
        /// </summary>
        public static List<int> GeneralWashCardCouponIds
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("GeneralWashCardCouponIds");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }
        /// <summary>
        /// 白金会员卡送券数量
        /// </summary>
        public static List<int> GeneralWashCardCouponCounts
        {
            get
            {
                var list = ConfigHelper.GetAppSettingList("GeneralWashCardCouponCounts");
                return list.Select(e => int.Parse(e)).ToList();
            }
        }


        public static int[] TireCheckCatIds = new int[] { 2601, 2701, 2801 };

        public static int GeneralWashAllinaceId = ConfigHelper.GetAppSettingInt("GeneralWashAllinaceId", -1);
        internal static string UserStatusPlatinumCardNoKey = "PlatinumCardNo";
        /// <summary>
        /// 最近使用的查询兴趣
        /// </summary>
        public static string UserStatusRecentQueryInterestKey = "RctQInterest";
        internal static string ShareActivityUrl = ConfigHelper.GetAppSetting("ShareActivityUrl");
        public static string InsuranceH5Url = ConfigHelper.GetAppSetting("InsuranceH5Url");
        internal static string ShareMomentUrl = ConfigHelper.GetAppSetting("ShareMomentUrl");
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
