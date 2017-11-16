using lvwei8.Service.Account.DTO;
using lvwei8.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Account
{
    public enum UserTradeType
    {
        ReviewsDD = 1,
        ReviewsCP = 2,
        ReviewsRC = 3,
        ComplaintDD = 4,
        ComplaintCP = 5,
        ComplaintRC = 6,
        TimesDD = 7,
        TimesCP = 8,
        TimesRC = 9,
    }

    /// <summary>
    /// 账户服务接口
    /// </summary>
    public interface IAccountService
    {
        #region 用户相关接口

        /// <summary>
        /// 用户是否已激活
        /// </summary>
        /// <param name="phone">用户手机号</param>
        /// <returns>是否激活</returns>
        bool IsActivity(string phone);
        /// <summary>
        /// 用户是否已激活
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsActivity(int userId);

        /// <summary>
        /// 从推广中选择上线
        /// </summary>
        /// <param name="phone">当前用户手机号</param>
        void SelectUplineFromPromotionAction(string phone);

        /// <summary>
        /// 记录推广行为
        /// </summary>
        /// <param name="userViewModel">用户视图模型</param>
        /// <param name="enumToActivityUserSourceType">来源</param>
        /// <param name="sendDefaultPassSms">是否发送默认短信</param>
        void LogPromotionAction(UserViewModel userViewModel, string enumToActivityUserSourceType, bool sendDefaultPassSms = true);



        /// <summary>
        /// 注册为待激活用户
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="enumToActivityUserSourceType"></param>
        /// <param name="sendDefaultPassSms"></param>
        /// <returns></returns>
        int RegisterAsToActivity(UserViewModel userViewModel, string enumToActivityUserSourceType, bool sendDefaultPassSms = true);

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="userViewModel">用户视图模型</param>
        /// <param name="checkVerificationCode">是否验证验证码</param>
        /// <returns>注册响应模型</returns>
        int Register(UserViewModel userViewModel, bool checkVerificationCode = true);



        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPassword">老密码</param>
        /// <param name="newPassword">新密码</param>
        void ChangePassword(string userName, string oldPassword, string newPassword);

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="verificationCode">手机验证码</param>
        /// <param name="newPassword">新密码</param>
        void ForgetPassword(string userName, string verificationCode, string newPassword);


        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户信息</returns>
        UserViewModel GetByUserId(int userId);

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        UserViewModel GetByPhone(string phone = "");

        /// <summary>
        /// 通过车牌号查询用户信息
        /// </summary>
        /// <param name="carNumber">车牌号</param>
        /// <returns></returns>
        List<UserViewModel> QueryByCarNumber(string carNumber);

        /// <summary>
        /// 根据手机号获取用户中的推广信息
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns>用户中的推广信息</returns>
        UserViewModel GetPromotionByPhone(string phone);

        /// <summary>
        /// 获取用户上线
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户上线</returns>
        UserViewModel GetUpline(int userId);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>判断用户是否存在</returns>
        bool IsExist(string userName);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>判断用户是否存在</returns>
        bool IsExist(int userId);



        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="userName">用户名</param>
        void UpdateUserName(int userId, string userName);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="password">密码</param>
        void UpdatePassword(int userId, string password);

        /// <summary>
        /// 更新专家相关信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="birth">出生日期</param>
        void UpdateExpert(int userId, string realName, DateTime? birth);


        /// <summary>
        /// 根据手机号批量查询用户
        /// </summary>
        /// <param name="phones">手机号列表</param>
        /// <returns>用户列表</returns>
        IEnumerable<UserViewModel> BatchQueryByPhones(IEnumerable<string> phones);

      

        ///// <summary>
        ///// 更新用户基本信息
        ///// </summary>
        ///// <param name="user">用户模型</param>
        //void UpdateBasic(UserViewModel user);

        /// <summary>
        /// 查询最近注册用户
        /// </summary>
        /// <returns></returns>
        List<UserViewModel> QueryRecentlyRegiste();

        /// <summary>
        /// 获取所有昵称
        /// </summary>
        /// <returns></returns>
        List<string> GetNickNames();

        #endregion




 




    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="filterSortMap"></param>
        /// <param name="phones"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        List<UserViewModel> Query(List<int> userIds, string phones, FilterSortMap filterSortMap, ref PageModel page);

        void ChangePasswordB(string userName, string newPassword);

        /// <summary>
        /// 设定返现
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <param name="cashbackOrderId"></param>
        void AssignCashback(int userId, decimal amount, int cashbackOrderId);
        /// <summary>
        /// 能否返现
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        bool CanCashback(int userId);
        /// <summary>
        /// 返现
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        void Cashback(int userId);
        /// <summary>
        /// 重置后台区域的未激活的账户密码
        /// </summary>
        /// <param name="AreaCod">区域码</param>
        /// <param name="userName">用户名</param>
        /// <param name="newPassword">密码</param>

        void ChangePasswordNoActiveB(string AreaCod, string userName, string newPassword);







        int? GetUserIdByPhone(string phone);




    }
}
