using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lvwei8.Service.Account.DTO;
using lvwei8.Service.Base;

namespace lvwei8.Service.Account
{
    public class AccountServiceImpl : IAccountService
    {
        public void AssignCashback(int userId, decimal amount, int cashbackOrderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserViewModel> BatchQueryByPhones(IEnumerable<string> phones)
        {
            throw new NotImplementedException();
        }

        public bool CanCashback(int userId)
        {
            throw new NotImplementedException();
        }

        public void Cashback(int userId)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void ChangePasswordB(string userName, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void ChangePasswordNoActiveB(string AreaCod, string userName, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void ForgetPassword(string userName, string verificationCode, string newPassword)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetByPhone(string phone = "")
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetNickNames()
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetPromotionByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUpline(int userId)
        {
            throw new NotImplementedException();
        }

        public int? GetUserIdByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public bool IsActivity(int userId)
        {
            throw new NotImplementedException();
        }

        public bool IsActivity(string phone)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int userId)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(string userName)
        {
            throw new NotImplementedException();
        }

        public void LogPromotionAction(UserViewModel userViewModel, string enumToActivityUserSourceType, bool sendDefaultPassSms = true)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> Query(List<int> userIds, string phones, FilterSortMap filterSortMap, ref PageModel page)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> QueryByCarNumber(string carNumber)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> QueryRecentlyRegiste()
        {
            throw new NotImplementedException();
        }

        public int Register(UserViewModel userViewModel, bool checkVerificationCode = true)
        {
            throw new NotImplementedException();
        }

        public int RegisterAsToActivity(UserViewModel userViewModel, string enumToActivityUserSourceType, bool sendDefaultPassSms = true)
        {
            throw new NotImplementedException();
        }

        public void SelectUplineFromPromotionAction(string phone)
        {
            throw new NotImplementedException();
        }

        public void UpdateExpert(int userId, string realName, DateTime? birth)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassword(int userId, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserName(int userId, string userName)
        {
            throw new NotImplementedException();
        }
    }
}
