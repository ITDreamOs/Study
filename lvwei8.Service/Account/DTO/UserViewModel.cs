using lvwei8.Common.Helpers;
using lvwei8.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Account.DTO
{
    /// <summary>
    /// 用户的基本信息
    /// </summary>
    public class UserBaseViewModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名(手机号)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 个人头像
        /// </summary>
        public List<PicStorage> HeadPortrait { get; set; }
        /// <summary>
        /// 前端显示用的昵称
        /// </summary>
        public string NickNameForView
        {
            get
            {
                return string.IsNullOrWhiteSpace(NickName) ? StringHelper.MaskPhone(UserName) : NickName;
            }
        }
    }

    /// <summary>
    /// 用户社交信息
    /// </summary>
    public class UserForSocialViewModel : UserBaseViewModel
    {
        /// <summary>
        /// 生日(只存储公历)
        /// </summary>
        public DateTime? Birth { get; set; }
        /// <summary>
        /// 前端展示用字段,标志用户选择的类型, Birth只存储公历,前端根据该字段决定展示公历或阴历
        /// </summary>
        public bool IsLunarBirth { get; set; }
        /// <summary>
        /// 星座
        /// </summary>
        public string Constellation { get; set; }
        /// <summary>
        /// 属相
        /// </summary>
        public string Zodiac { get; set; }
        /// <summary>
        /// 是否已婚（非单身）
        /// </summary>
        public bool Married { get; set; }
        private string _bloodType;

        /// <summary>
        /// 真实年龄, 当出生日期未填写时,年龄为null
        /// </summary>
        public int? Age
        {
            get
            {
                if (!Birth.HasValue) return null;
                //DateTime birthDate = Birth.Value;
                //// 如果是阴历,转公历后再计算
                //if (IsLunarBirth)
                //{
                //    var calendar = new ChineseCalendar(Birth.Value.Year, Birth.Value.Month, Birth.Value.Day, false);
                //    birthDate = calendar.Date;
                //}
                return IdNumberHelper.GetAge(Birth.Value);
            }
        }
        /// <summary>
        /// 爱好
        /// </summary>
        public List<string> Interests { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public double? Distance { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string CarCode { get; set; }
        /// <summary>
        /// 车辆品牌名称
        /// </summary>
        public string CarCodeName { get; set; }
        /// <summary>
        /// 是否隐私
        /// </summary>
        public bool IsPrivacy { get; set; }
        /// <summary>
        /// 区域码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 区域名
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// Mood
        /// </summary>
        public string Mood { get; set; }
        /// <summary>
        /// 是否可以进行契合测试分享
        /// </summary>
        public bool CanShareFateTest
        {
            get
            {
                return //!string.IsNullOrWhiteSpace(BloodType) &&
                    !string.IsNullOrWhiteSpace(Zodiac)
                    && !string.IsNullOrWhiteSpace(Constellation)
                    && !string.IsNullOrWhiteSpace(Sex);
            }
        }
        /// <summary>
        /// 车牌号（带掩码）
        /// </summary>
        public string CarNumberForView { get; set; }
        /// <summary>
        /// 是否群成员
        /// </summary>
        public bool IsGroupMember { get; set; }
    }
    /// <summary>
    /// 用户视图模型
    /// </summary>
    public class UserViewModel : UserForSocialViewModel
    {
        /// <summary>
        /// 密码，只在创建帐户时使用
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// EMail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public System.DateTime CreateDate { get; set; }

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime? FirstLogin { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime? LasterLogin { get; set; }

        /// <summary>
        /// 最后登录终端
        /// </summary>
        public TerminalSource? TerminalSource { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 驾驶开始年份
        /// </summary>
        public int? DriverStartYear { get; set; }

        /// <summary>
        /// 驾龄
        /// </summary>
        public int DriverYears { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }



        /// <summary>
        /// 推荐人
        /// </summary>
        public int? Introducer { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 身份证图片
        /// </summary>
        public List<PicStorage> IdNumberPic { get; set; }

        /// <summary>
        /// 身份证认证状态
        /// </summary>
        public string IdNumberAuthStatus { get; set; }

        /// <summary>
        /// 交强险图片
        /// </summary>
        public List<PicStorage> CompulsoryInsurancePic { get; set; }

        /// <summary>
        /// 交强险认证状态,关联EnumApplyStatus
        /// </summary>
        public string CompulsoryInsuranceAuthStatus { get; set; }

        /// <summary>
        /// 驾驶证图片
        /// </summary>
        public List<PicStorage> DriverLicensePic { get; set; }

        /// <summary>
        /// 驾驶证认证状态,关联EnumApplyStatus
        /// </summary>
        public string DriverLicenseAuthStatus { get; set; }

        /// <summary>
        /// 行驶证
        /// </summary>
        public List<PicStorage> DrivingLicensePic { get; set; }

        /// <summary>
        /// 行驶证认证状态,关联EnumApplyStatus
        /// </summary>
        public string DrivingLicenseAuthStatus { get; set; }

        /// <summary>
        /// 汽车类型
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 车架号
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNumber { get; set; }

        /// <summary>
        /// 车辆图片
        /// </summary>
        public List<PicStorage> CarPics { get; set; }

        /// <summary>
        /// 用户联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 保险修改次数	
        /// </summary>
        public int CP_InsuranceUpdateTimes { get; set; }

        /// <summary>
        /// 作为车主拼车次数
        /// </summary>
        public int CP_CarOwnerTimes { get; set; }

        /// <summary>
        /// 作为乘客拼车次数
        /// </summary>
        public int CP_PassengerTimes { get; set; }

        /// <summary>
        /// 作为车主拼车违约次数
        /// </summary>
        public int CP_CarOwnerBrokenTimes { get; set; }

        /// <summary>
        /// 作为乘客拼车违约次数
        /// </summary>
        public int CP_PassengerBrokenTimes { get; set; }

        /// <summary>
        /// 作为车主拼车收到的评价次数
        /// </summary>
        public int CP_CarOwnerReceiveReviewTimes { get; set; }

        /// <summary>
        /// 作为乘客拼车收到的评价次数
        /// </summary>
        public int CP_PassengerReceiveReviewTimes { get; set; }

        /// <summary>
        /// 作为车主拼车的总评分
        /// </summary>
        public int CP_CarOwnerScores { get; set; }

        /// <summary>
        /// 作为乘客拼车的总评分
        /// </summary>
        public int CP_PassengerScores { get; set; }

        /// <summary>
        /// 作为车主代驾次数
        /// </summary>
        public int DD_CarOwnerTimes { get; set; }

        /// <summary>
        /// 作为乘客代驾次数
        /// </summary>
        public int DD_PassengerTimes { get; set; }

        /// <summary>
        /// 作为车主代驾违约次数
        /// </summary>
        public int DD_CarOwnerBrokenTimes { get; set; }

        /// <summary>
        /// 作为乘客代驾违约次数
        /// </summary>
        public int DD_PassengerBrokenTimes { get; set; }

        /// <summary>
        /// 代驾:服务接收者收到的评价次数
        /// </summary>
        public int DD_CarOwnerReceiveReviewTimes { get; set; }

        /// <summary>
        /// 代驾:服务提供者收到的评价次数
        /// </summary>
        public int DD_PassengerReceiveReviewTimes { get; set; }

        /// <summary>
        /// 作为车主代驾的总评分
        /// </summary>
        public int DD_CarOwnerScores { get; set; }

        /// <summary>
        /// 作为乘客代驾的总评分
        /// </summary>
        public int DD_PassengerScores { get; set; }

        /// <summary>
        /// 出租者 出租次数
        /// </summary>
        public int RC_LessorTimes { get; set; }

        /// <summary>
        /// 承租者 租车次数
        /// </summary>
        public int RC_RenterTimes { get; set; }

        /// <summary>
        /// 出租车违约次数
        /// </summary>
        public int RC_LessorBrokenTimes { get; set; }

        /// <summary>
        /// 承租者 违约次数
        /// </summary>
        public int RC_RenterBrokenTimes { get; set; }

        /// <summary>
        /// 出租者 被评论次数
        /// </summary>
        public int RC_LessorReceiveReviewTimes { get; set; }

        /// <summary>
        /// 承租者 被评论次数
        /// </summary>
        public int RC_RenterReceiveReviewTimes { get; set; }

        /// <summary>
        /// 出租者 被评分数
        /// </summary>
        public int RC_LessorScores { get; set; }

        /// <summary>
        /// 承租者 被评分数
        /// </summary>
        public int RC_RenterScores { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// 图形验证码
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 生成图形验证码时附带的sessionid
        /// </summary>
        public string CaptchaSessionId { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }

        /// <summary>
        /// 上线用户名
        /// </summary>
        public string UplineUserName { get; set; }

        /// <summary>
        /// 上线id,即推荐人id
        /// </summary>
        public int? UplineId { get; set; }

        /// <summary>
        /// 下线数量(即粉丝数)
        /// </summary>
        public long DownlineCount
        {
            get
            {
                return this.DownlineLv1Count + this.DownlineLv2Count + this.DownlineLv3Count;
            }
        }

        /// <summary>
        /// 一级下线
        /// </summary>
        public string DownlineLv1 { get; set; }

        /// <summary>
        /// 二级下线
        /// </summary>
        public string DownlineLv2 { get; set; }

        /// <summary>
        /// 三级下线
        /// </summary>
        public string DownlineLv3 { get; set; }

        /// <summary>
        /// 一级粉丝数
        /// </summary>
        public long DownlineLv1Count { get; set; }

        /// <summary>
        /// 已激活一级粉丝数量
        /// </summary>
        public long ActiveDownlineLv1Count { get; set; }

        /// <summary>
        /// 二级粉丝数
        /// </summary>
        public long DownlineLv2Count { get; set; }

        /// <summary>
        /// 已激活二级粉丝数量
        /// </summary>
        public long ActiveDownlineLv2Count { get; set; }

        /// <summary>
        /// 三级粉丝数
        /// </summary>
        public long DownlineLv3Count { get; set; }

        /// <summary>
        /// 累计收益
        /// </summary>
        public decimal Income { get; internal set; }

        /// <summary>
        /// IMUserId
        /// </summary>
        public string IMUserId { get; set; }

        /// <summary>
        /// 是否已同意保险协议
        /// </summary>
        public bool CarpoolInsureAgreed { get; set; }
        /// <summary>
        /// 是否已禁止修改
        /// </summary>
        public bool CarpoolInsureModifyDeinied { get; set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime? CarpoolInsureLastModifyDate { get; set; }

        #region Car
        /// <summary>
        /// 变速箱
        /// </summary>
        public string GearBox { get; set; }
        /// <summary>
        /// 行驶里程数,枚举EnumRentalSeatCountNeed(前端界面使用)
        /// </summary>
        public string DrivingMileage { get; set; }

        /// <summary>
        /// 座位数,枚举EnumRentalSeatCountNeed(前端界面使用)
        /// </summary>
        public string SeatCount { get; set; }

        /// <summary>
        /// 车辆颜色, 关联到CarColor
        /// </summary>
        public string CarColor { get; set; }
        #endregion
        /// <summary>
        /// 是否已设置过支付密码, false表示没有设置过
        /// </summary>
        public bool HavePaymentPassword { get; set; }
        /// <summary>
        /// 等级描述
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 等级数
        /// </summary>
        public int LevelNum { get; set; }

        /// <summary>
        /// 首次登陆商家端时间
        /// </summary>
        public DateTime? FirstLoginStoreDate { get; set; }
        /// <summary>
        /// 最后登录商家端时间
        /// </summary>
        public DateTime? LasterLoginStoreDate { get; set; }
        /// <summary>
        /// 首次登陆配件商时间
        /// </summary>
        public DateTime? FirstLoginAcrStoreDate { get; set; }
        /// <summary>
        /// 最后登录配件商时间
        /// </summary>
        public DateTime? LasterLoginAcrStoreDate { get; set; }

        /// <summary>
        /// 用户登录次数
        /// </summary>
        public int? LoginTimes { get; set; }
        /// <summary>
        /// 是否已激活
        /// </summary>
        public bool IsActivity
        {
            get
            {
                return this.LasterLogin.HasValue;
            }
        }


        /// <summary>
        /// 是否已开通Dudu电话
        /// </summary>
        public bool IsDuduUser { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? DuduExpireDate { get; internal set; }
        /// <summary>
        /// 嘟嘟默认每月充值钱数
        /// </summary>
        public double DuduDefaultAmount { get; set; }
        /// <summary>
        /// 畅聊上线id
        /// </summary>
        public int? ChatUplineId { get; set; }
        /// <summary>
        /// 平级分数
        /// </summary>
        public int Score { get; set; }


        /// <summary>
        ///车故障
        /// </summary>
        public bool IsCarEngine { get; set; }
        /// <summary>
        /// 已绑定会员卡，则返回白金卡号
        /// </summary>
        public string PlatinumCardNo { get; set; }
        /// <summary>
        /// 是否已激活
        /// </summary>
        public bool IsPlatinumMemberActived { get; set; }

        #region 后台统计用
        /// <summary>
        /// 总订单量
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 是否正在等待申请的白金会员卡已分配
        /// </summary>
        public bool IsWaitingPlatinumCard { get; internal set; }
        #endregion
    }
    }
