using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginControl.Model;
using LoginControl.DataAcessLayer;

namespace LoginControl.BusinessLayer
{
    public partial class UserBLL
    {


        UserDAL objUserDAL = new UserDAL();

        public UserBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        ///  Method for getting user details 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserDetailsById(int userId)
        {
            return objUserDAL.GetUserDetailsById(userId);
        }

        /// <summary>
        /// Method for getting Userdetails
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserDetails()
        {
            return objUserDAL.GetUserDetails();
        }

        /// <summary>
        ///  Method for add Userdetails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUserDetails(User user)
        {
            return objUserDAL.AddUserDetails(user);
        }

        /// <summary>
        /// Method for updating Userdetails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUserDetails(User user)
        {
            return objUserDAL.UpdateUserDetails(user);
        }

        /// <summary>
        ///  Method for updating User status
        /// </summary>
        /// <param name="objAuditLog"></param>
        /// <param name="GudId"></param>
        /// <returns></returns>
        public bool UpdateUserStatus(AuditLog objAuditLog, string gudId)
        {
            return objUserDAL.UpdateUserStatus(objAuditLog, gudId);
        }

        /// <summary>
        /// Method for getting User details by Email id
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserDetailsByEmailId(string email)
        {
            return objUserDAL.GetUserDetailsByEmailId(email);
        }

        /// <summary>
        /// Method for updating password
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <param name="GudId"></param>
        /// <returns></returns>
        public bool UpdatePassword(UserLogin objUserLogin, string gudId)
        {
            return objUserDAL.UpdatePassword(objUserLogin, gudId);
        }

        /// <summary>
        /// Method for getting user details by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserDetailsByUserName(string userName)
        {
            return objUserDAL.GetUserDetailsByUserName(userName);
        }

        /// <summary>
        ///  Method for lock user details
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <returns></returns>
        public bool LockUserDetails(UserLogin objUserLogin)
        {
            return objUserDAL.LockUserDetails(objUserLogin);
        }

        /// <summary>
        /// Method for updating password wrong attempts
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <returns></returns>
        public bool UpdatePasswordWorngAttemptDetails(UserLogin objUserLogin)
        {
            return objUserDAL.UpdatePasswordWorngAttemptDetails(objUserLogin);
        }

        /// <summary>
        /// Method for getting password wrong attempts
        /// </summary>
        /// <returns></returns>
        public LoginAttempts GetLoginAttempts()
        {
            return objUserDAL.GetLoginAttempts();
        }

        /// <summary>
        /// Method for updating login attempts
        /// </summary>
        /// <param name="objLoginAttempts"></param>
        /// <returns></returns>
        public bool UpdateLoginAttempts(LoginAttempts objLoginAttempts)
        {
            return objUserDAL.UpdateLoginAttempts(objLoginAttempts);
        }

        /// <summary>
        /// Method for clearing login attempts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ClearLoginAttempts(int userId)
        {
            return objUserDAL.ClearLoginAttempts(userId);
        }

        /// <summary>
        ///  Method for getting user details by username
        /// </summary>
        /// <param name="searchUser"></param>
        /// <returns></returns>
        public List<User> GetSearchUserDetailsByUserName(string searchUser)
        {
            return objUserDAL.GetSearchUserDetailsByUserName(searchUser);
        }

        /// <summary>
        ///  Method for logging login time
        /// </summary>
        /// <param name="objLoginLogoutHistory"></param>
        /// <returns></returns>
        public bool LogLoginTime(LoginHistory objLoginLogoutHistory)
        {
            return objUserDAL.LogLoginTime(objLoginLogoutHistory);
        }

        /// <summary>
        ///  Method for logging logout time
        /// </summary>
        /// <param name="objLoginLogoutHistory"></param>
        /// <returns></returns>
        public bool LogLogoutTime(LoginHistory objLoginLogoutHistory)
        {
            return objUserDAL.LogLogoutTime(objLoginLogoutHistory);
        }

        /// <summary>
        /// Method for getting login history
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<LoginHistory> GetLoginHistory(string userName)
        {
            return objUserDAL.GetLoginHistory(userName);
        }

        /// <summary>
        ///  Method for updating user password
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <returns></returns>
        public bool UpdateUserPassword(UserLogin objUserLogin)
        {
            return objUserDAL.UpdateUserPassword(objUserLogin);
        }

    }
}