using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginControl.Model;
using log4net;
using System.Data.SqlClient;
using System.Data;
using LoginControl.Utilities;
using LoginControl.Logs;

namespace LoginControl.DataAcessLayer
{
    public partial class UserDAL
    {


        #region Stored Procedure Names

        ILog log = LogManager.GetLogger("UserDAL");
        const string COMMONDATA = "UserDAL";
        private readonly string SP_GetUserDetailsById = "usp_GetUserDetailsById";
        private readonly string SP_GetUserDetails = "usp_GetUserDetails";
        private readonly string SP_AddUserDetails = "usp_AddUserDetails";
        private readonly string SP_UpdateUserStatus = "usp_UpdateUserStatus";
        private readonly string SP_GetUserDetailsByEmailId = "usp_GetUserDetailsByEmailId";
        private readonly string SP_UpdatePassword = "usp_UpdatePassword";
        private readonly string SP_GetUserDetailsByUserName = "usp_GetUserDetailsByUserName";
        private readonly string SP_LockUserDetails = "usp_LockUserDetails";
        private readonly string SP_UpdatePasswordWorngAttemptDetails = "usp_UpdatePasswordWorngAttemptDetails";
        private const string SP_GetLoginAttempts = "usp_GetLoginAttempts";
        private const string SP_UpdateLoginAttempts = "usp_UpdateLoginAttempts";
        private const string SP_ClearLoginAttempts = "usp_ClearLoginAttempts";
        private readonly string SP_GetSearchUserDetailsByUserName = "usp_GetSearchUserDetailsByUserName";
        private const string SP_LogLoginTime = "usp_LogLoginTime";
        private const string SP_LogLogoutTime = "usp_LogLogoutTime";
        private const string SP_GetLoginHistory = "usp_GetLoginHistory";
        private const string SP_UpdateUserPassword = "usp_UpdateUserPassword";

        #endregion

        public UserDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Method for getting user details by userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserDetailsById(int userId)
        {
            User objUser = new User();
            UserLogin objUserLogin = new UserLogin();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetUserDetailsById, new SqlParameter("@userId", userId)))
                {
                    if (reader.Read())
                    {
                        objUser.Id = Convert.ToInt32(reader["UserId"]);
                        objUser.FirstName = Convert.ToString(reader["FirstName"]);
                        objUser.LastName = Convert.ToString(reader["LastName"]);
                        objUser.Mobile = Convert.ToString(reader["Mobile"]);
                        objUser.Gender = Convert.ToInt32(reader["Gender"]);
                        objUser.Address = Convert.ToString(reader["Address"]);
                        objUser.CountryId = Convert.ToInt32(reader["CountryId"]);
                        objUser.City = Convert.ToString(reader["City"]);
                        objUser.ZipCode = Convert.ToString(reader["ZipCode"]);
                        objUser.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                        objUser.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                        objUser.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                        objUser.UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]);
                        objUser.IsEmailVerified = Convert.ToBoolean(reader["IsEmailVerified"]);
                        objUserLogin.UserName = Convert.ToString(reader["UserName"]);
                        objUserLogin.Password = Convert.ToString(reader["Password"]);
                        objUserLogin.SecurityQuestion = Convert.ToInt32(reader["SecurityQuestion"]);
                        objUserLogin.Answer = Convert.ToString(reader["Answer"]);
                        objUserLogin.PasswordWrongAttempts = Convert.ToInt32(reader["PasswordWrongAttempts"]);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(reader["LastPasswordWrong"])))
                        {
                            objUserLogin.LastPasswordWrong = Convert.ToDateTime(reader["LastPasswordWrong"]);
                        }
                        else
                        {
                            objUserLogin.LastPasswordWrong = null;
                        }
                        objUserLogin.RoleId = Convert.ToInt32(reader["RoleId"]);
                        objUserLogin.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        objUserLogin.UserId = Convert.ToInt32(reader["UserId"]);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(reader["AccountLocked"])))
                        {
                            objUserLogin.AccountLocked = Convert.ToBoolean(reader["AccountLocked"]);
                        }
                        else
                        {
                            objUserLogin.AccountLocked = false;
                        }
                        objUser.UserLogin = objUserLogin;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetUserDetailsById \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetUserDetailsById ", ex.Message + " \n " + ex.StackTrace);
            }
            return objUser;
        }

        /// <summary>
        /// Method for getting user details 
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserDetails()
        {
            List<User> objLstUser = new List<User>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                using (DataSet objUserDataSet = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetUserDetails))
                {
                    if (objUserDataSet.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < objUserDataSet.Tables[0].Rows.Count; i++)
                        {
                            User objUser = new User();
                            UserLogin objUserLogin = new UserLogin();

                            objUser.Id = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UserId"]);
                            objUser.FirstName = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["FirstName"]);
                            objUser.LastName = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["LastName"]);
                            objUser.Mobile = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Mobile"]);
                            objUser.Gender = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["Gender"]);
                            objUser.Address = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Address"]);
                            objUser.CountryId = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["CountryId"]);
                            objUser.Email = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Email"]);
                            objUser.City = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["City"]);
                            objUser.ZipCode = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["ZipCode"]);
                            objUser.CreatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["CreatedBy"]);
                            objUser.CreatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["CreatedOn"]);
                            objUser.UpdatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UpdatedBy"]);
                            objUser.UpdatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["UpdatedOn"]);
                            objUser.IsEmailVerified = Convert.ToBoolean(objUserDataSet.Tables[0].Rows[i]["IsEmailVerified"]);
                            objUserLogin.UserName = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["UserName"]);
                            objUserLogin.Password = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Password"]);
                            objUserLogin.SecurityQuestion = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["SecurityQuestion"]);
                            objUserLogin.Answer = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Answer"]);
                            objUserLogin.PasswordWrongAttempts = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["PasswordWrongAttempts"]);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["LastPasswordWrong"])))
                            {
                                objUserLogin.LastPasswordWrong = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["LastPasswordWrong"]);
                            }
                            else
                            {
                                objUserLogin.LastPasswordWrong = null;
                            }
                            objUserLogin.CreatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["CreatedBy"]);
                            objUserLogin.CreatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["CreatedOn"]);
                            objUserLogin.UpdatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UpdatedBy"]);
                            objUserLogin.UpdatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["UpdatedOn"]);
                            objUserLogin.RoleId = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["RoleId"]);
                            objUserLogin.IsActive = Convert.ToBoolean(objUserDataSet.Tables[0].Rows[i]["IsActive"]);
                            objUserLogin.UserId = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UserId"]);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["AccountLocked"])))
                            {
                                objUserLogin.AccountLocked = Convert.ToBoolean(objUserDataSet.Tables[0].Rows[i]["AccountLocked"]);
                            }
                            else
                            {
                                objUserLogin.AccountLocked = false;
                            }
                            objUser.UserLogin = objUserLogin;
                            objLstUser.Add(objUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetUserDetails \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetUserDetails ", ex.Message + " \n " + ex.StackTrace);
            }
            return objLstUser;
        }

        /// <summary>
        /// Method for inserting user details by userid
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUserDetails(User user)
        {
            int result = 0;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[20];

                SqlParameter objFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar);
                objFirstName.Value = user.FirstName;
                objLstParams[0] = objFirstName;

                SqlParameter objLastName = new SqlParameter("@LastName", SqlDbType.VarChar);
                objLastName.Value = user.LastName;
                objLstParams[1] = objLastName;

                SqlParameter objMobile = new SqlParameter("@Mobile", SqlDbType.VarChar);
                objMobile.Value = user.Mobile;
                objLstParams[2] = objMobile;

                SqlParameter objEmail = new SqlParameter("@Email", SqlDbType.VarChar);
                objEmail.Value = user.Email;
                objLstParams[3] = objEmail;

                SqlParameter objGender = new SqlParameter("@Gender", SqlDbType.Int);
                objGender.Value = user.Gender;
                objLstParams[4] = objGender;

                SqlParameter objAddress = new SqlParameter("@Address", SqlDbType.VarChar);
                objAddress.Value = user.Address;
                objLstParams[5] = objAddress;

                SqlParameter objCountryId = new SqlParameter("@CountryId", SqlDbType.Int);
                objCountryId.Value = user.CountryId;
                objLstParams[6] = objCountryId;

                SqlParameter objCity = new SqlParameter("@City", SqlDbType.VarChar);
                objCity.Value = user.City;
                objLstParams[7] = objCity;

                SqlParameter objZipCode = new SqlParameter("@ZipCode", SqlDbType.VarChar);
                objZipCode.Value = user.ZipCode;
                objLstParams[8] = objZipCode;

                SqlParameter objCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
                objCreatedBy.Value = user.CreatedBy;
                objLstParams[9] = objCreatedBy;

                SqlParameter objCreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                objCreatedOn.Value = user.CreatedOn;
                objLstParams[10] = objCreatedOn;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = user.UpdatedBy;
                objLstParams[11] = objUpdatedBy;

                SqlParameter objUpdatedOn = new SqlParameter("@UpdatedOn", SqlDbType.DateTime);
                objUpdatedOn.Value = user.UpdatedOn;
                objLstParams[12] = objUpdatedOn;

                SqlParameter objIsEmailVerified = new SqlParameter("@IsEmailVerified", SqlDbType.Bit);
                objIsEmailVerified.Value = user.IsEmailVerified;
                objLstParams[13] = objIsEmailVerified;

                SqlParameter objUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
                objUserName.Value = user.UserLogin.UserName;
                objLstParams[14] = objUserName;


                SqlParameter objPassword = new SqlParameter("@Password", SqlDbType.VarChar);
                objPassword.Value = user.UserLogin.Password;
                objLstParams[15] = objPassword;

                SqlParameter objSecurityQuestion = new SqlParameter("@SecurityQuestion", SqlDbType.Int);
                objSecurityQuestion.Value = user.UserLogin.SecurityQuestion;
                objLstParams[16] = objSecurityQuestion;

                SqlParameter objAnswer = new SqlParameter("@Answer", SqlDbType.VarChar);
                objAnswer.Value = user.UserLogin.Answer;
                objLstParams[17] = objAnswer;

                SqlParameter objRoleId = new SqlParameter("@RoleId", SqlDbType.Int);
                objRoleId.Value = user.UserLogin.RoleId;
                objLstParams[18] = objRoleId;

                SqlParameter objIsActive = new SqlParameter("@IsActive", SqlDbType.Bit);
                objIsActive.Value = user.UserLogin.IsActive;
                objLstParams[19] = objIsActive;

                result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_AddUserDetails, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("AddUserDetails \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ AddUserDetails ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for upadting user details 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUserDetails(User user)
        {
            bool result = false;
            try
            {

            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// Method for updating user status
        /// </summary>
        /// <param name="objAuditLog"></param>
        /// <param name="GudId"></param>
        /// <returns></returns>
        public bool UpdateUserStatus(AuditLog objAuditLog, string gudId)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[2];

                SqlParameter objGudId = new SqlParameter("@GudId", SqlDbType.VarChar);
                objGudId.Value = Convert.ToString(gudId);
                objLstParams[0] = objGudId;

                SqlParameter objUserId = new SqlParameter("@UserId", SqlDbType.Int);
                objUserId.Value = Convert.ToString(objAuditLog.UserId);
                objLstParams[1] = objUserId;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_UpdateUserStatus, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("UpdateUserStatus \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ UpdateUserStatus ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for getting user details by email id
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserDetailsByEmailId(string email)
        {
            User objUser = new User();
            UserLogin objUserLogin = new UserLogin();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetUserDetailsByEmailId, new SqlParameter("@email", email)))
                {
                    if (reader.Read())
                    {
                        objUser.Id = Convert.ToInt32(reader["UserId"]);
                        objUser.FirstName = Convert.ToString(reader["FirstName"]);
                        objUser.LastName = Convert.ToString(reader["LastName"]);
                        objUser.Mobile = Convert.ToString(reader["Mobile"]);
                        objUser.Gender = Convert.ToInt32(reader["Gender"]);
                        objUser.Address = Convert.ToString(reader["Address"]);
                        objUser.CountryId = Convert.ToInt32(reader["CountryId"]);
                        objUser.City = Convert.ToString(reader["City"]);
                        objUser.ZipCode = Convert.ToString(reader["ZipCode"]);
                        objUser.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                        objUser.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                        objUser.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                        objUser.UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]);
                        objUser.IsEmailVerified = Convert.ToBoolean(reader["IsEmailVerified"]);
                        objUserLogin.UserName = Convert.ToString(reader["UserName"]);
                        objUserLogin.Password = Convert.ToString(reader["Password"]);
                        objUserLogin.SecurityQuestion = Convert.ToInt32(reader["SecurityQuestion"]);
                        objUserLogin.Answer = Convert.ToString(reader["Answer"]);
                        objUserLogin.PasswordWrongAttempts = Convert.ToInt32(reader["PasswordWrongAttempts"]);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(reader["LastPasswordWrong"])))
                        {
                            objUserLogin.LastPasswordWrong = Convert.ToDateTime(reader["LastPasswordWrong"]);
                        }
                        else
                        {
                            objUserLogin.LastPasswordWrong = null;
                        }
                        objUserLogin.RoleId = Convert.ToInt32(reader["RoleId"]);
                        objUserLogin.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        objUserLogin.UserId = Convert.ToInt32(reader["UserId"]);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(reader["AccountLocked"])))
                        {
                            objUserLogin.AccountLocked = Convert.ToBoolean(reader["AccountLocked"]);
                        }
                        else
                        {
                            objUserLogin.AccountLocked = false;
                        }
                        objUser.UserLogin = objUserLogin;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetUserDetailsByEmailId \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetUserDetailsByEmailId ", ex.Message + " \n " + ex.StackTrace);
            }
            return objUser;
        }

        /// <summary>
        /// Method for update password details
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <param name="GudId"></param>
        /// <returns></returns>
        public bool UpdatePassword(UserLogin objUserLogin, string gudId)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[4];

                SqlParameter objPassword = new SqlParameter("@Password", SqlDbType.VarChar);
                objPassword.Value = objUserLogin.Password;
                objLstParams[0] = objPassword;

                SqlParameter objUserId = new SqlParameter("@UserId", SqlDbType.Int);
                objUserId.Value = Convert.ToInt32(objUserLogin.UserId);
                objLstParams[1] = objUserId;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = Convert.ToInt32(objUserLogin.UpdatedBy);
                objLstParams[2] = objUpdatedBy;

                SqlParameter objGudId = new SqlParameter("@GudId", SqlDbType.VarChar);
                objGudId.Value = Convert.ToString(gudId);
                objLstParams[3] = objGudId;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_UpdatePassword, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("UpdatePassword \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ UpdatePassword ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        ///  Method for getting user details by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserDetailsByUserName(string userName)
        {
            User objUser = new User();
            UserLogin objUserLogin = new UserLogin();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetUserDetailsByUserName, new SqlParameter("@UserName", userName)))
                {
                    if (reader.Read())
                    {
                        objUser.Id = Convert.ToInt32(reader["UserId"]);
                        objUser.FirstName = Convert.ToString(reader["FirstName"]);
                        objUser.LastName = Convert.ToString(reader["LastName"]);
                        objUser.Mobile = Convert.ToString(reader["Mobile"]);
                        objUser.Gender = Convert.ToInt32(reader["Gender"]);
                        objUser.Address = Convert.ToString(reader["Address"]);
                        objUser.CountryId = Convert.ToInt32(reader["CountryId"]);
                        objUser.City = Convert.ToString(reader["City"]);
                        objUser.ZipCode = Convert.ToString(reader["ZipCode"]);
                        objUser.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                        objUser.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                        objUser.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                        objUser.UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]);
                        objUser.IsEmailVerified = Convert.ToBoolean(reader["IsEmailVerified"]);
                        objUserLogin.UserName = Convert.ToString(reader["UserName"]);
                        objUserLogin.Password = Convert.ToString(reader["Password"]);
                        objUserLogin.SecurityQuestion = Convert.ToInt32(reader["SecurityQuestion"]);
                        objUserLogin.Answer = Convert.ToString(reader["Answer"]);
                        objUserLogin.PasswordWrongAttempts = Convert.ToInt32(reader["PasswordWrongAttempts"]);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(reader["LastPasswordWrong"])))
                        {
                            objUserLogin.LastPasswordWrong = Convert.ToDateTime(reader["LastPasswordWrong"]);
                        }
                        else
                        {
                            objUserLogin.LastPasswordWrong = null;
                        }

                        objUserLogin.RoleId = Convert.ToInt32(reader["RoleId"]);
                        objUserLogin.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        objUserLogin.UserId = Convert.ToInt32(reader["UserId"]);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(reader["AccountLocked"])))
                        {
                            objUserLogin.AccountLocked = Convert.ToBoolean(reader["AccountLocked"]);
                        }
                        else
                        {
                            objUserLogin.AccountLocked = false;
                        }
                        objUser.UserLogin = objUserLogin;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetUserDetailsByUserName \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetUserDetailsByUserName ", ex.Message + " \n " + ex.StackTrace);
            }
            return objUser;
        }

        /// <summary>
        /// Methos for locking user
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <returns></returns>
        public bool LockUserDetails(UserLogin objUserLogin)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[6];

                SqlParameter objAccountLocked = new SqlParameter("@AccountLocked", SqlDbType.Bit);
                objAccountLocked.Value = objUserLogin.AccountLocked;
                objLstParams[0] = objAccountLocked;

                SqlParameter objPasswordWrongAttempts = new SqlParameter("@PasswordWrongAttempts", SqlDbType.Int);
                objPasswordWrongAttempts.Value = Convert.ToInt32(objUserLogin.PasswordWrongAttempts);
                objLstParams[1] = objPasswordWrongAttempts;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = Convert.ToInt32(objUserLogin.UpdatedBy);
                objLstParams[2] = objUpdatedBy;

                SqlParameter objUpdatedOn = new SqlParameter("@UpdatedOn", SqlDbType.DateTime);
                objUpdatedOn.Value = objUserLogin.UpdatedOn;
                objLstParams[3] = objUpdatedOn;

                SqlParameter objLastPasswordWrong = new SqlParameter("@LastPasswordWrong", SqlDbType.DateTime);
                objLastPasswordWrong.Value = objUserLogin.LastPasswordWrong;
                objLstParams[4] = objLastPasswordWrong;

                SqlParameter objUserId = new SqlParameter("@UserId", SqlDbType.Int);
                objUserId.Value = objUserLogin.LastPasswordWrong;
                objLstParams[5] = objUserId;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_LockUserDetails, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("LockUserDetails \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ LockUserDetails ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for updating wrong password attempts
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <returns></returns>
        public bool UpdatePasswordWorngAttemptDetails(UserLogin objUserLogin)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[6];

                SqlParameter objAccountLocked = new SqlParameter("@AccountLocked", SqlDbType.Bit);
                objAccountLocked.Value = objUserLogin.AccountLocked;
                objLstParams[0] = objAccountLocked;

                SqlParameter objPasswordWrongAttempts = new SqlParameter("@PasswordWrongAttempts", SqlDbType.Int);
                objPasswordWrongAttempts.Value = Convert.ToInt32(objUserLogin.PasswordWrongAttempts);
                objLstParams[1] = objPasswordWrongAttempts;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = Convert.ToInt32(objUserLogin.UpdatedBy);
                objLstParams[2] = objUpdatedBy;

                SqlParameter objUpdatedOn = new SqlParameter("@UpdatedOn", SqlDbType.DateTime);
                objUpdatedOn.Value = objUserLogin.UpdatedOn;
                objLstParams[3] = objUpdatedOn;

                SqlParameter objLastPasswordWrong = new SqlParameter("@LastPasswordWrong", SqlDbType.DateTime);
                if (objUserLogin.LastPasswordWrong == null)
                {
                    objLastPasswordWrong.Value = DBNull.Value;
                }
                else
                {
                    objLastPasswordWrong.Value = objUserLogin.LastPasswordWrong;
                }

                objLstParams[4] = objLastPasswordWrong;

                SqlParameter objUserId = new SqlParameter("@UserId", SqlDbType.Int);
                objUserId.Value = objUserLogin.UserId;
                objLstParams[5] = objUserId;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_UpdatePasswordWorngAttemptDetails, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("UpdatePasswordWorngAttemptDetails \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ UpdatePasswordWorngAttemptDetails ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for getting login attempts
        /// </summary>
        /// <returns></returns>
        public LoginAttempts GetLoginAttempts()
        {
            LoginAttempts objLoginAttempts = new LoginAttempts();
            try
            {
                using (DataSet loginAttemptsTable = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetLoginAttempts))
                {
                    if (loginAttemptsTable.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < loginAttemptsTable.Tables[0].Rows.Count; i++)
                        {
                            objLoginAttempts.Id = Convert.ToInt32(loginAttemptsTable.Tables[0].Rows[i]["Id"]);
                            objLoginAttempts.LoginAttempt = Convert.ToInt32(loginAttemptsTable.Tables[0].Rows[i]["LoginAttempts"]);
                            objLoginAttempts.CreatedBy = Convert.ToInt32(loginAttemptsTable.Tables[0].Rows[i]["CreatedBy"]);
                            objLoginAttempts.CreatedOn = Convert.ToDateTime(loginAttemptsTable.Tables[0].Rows[i]["CreatedOn"]);
                            objLoginAttempts.CreatedIp = Convert.ToString(loginAttemptsTable.Tables[0].Rows[i]["CreatedIp"]);
                            objLoginAttempts.UpdatedBy = Convert.ToInt32(loginAttemptsTable.Tables[0].Rows[i]["UpdatedBy"]);
                            objLoginAttempts.UpdatedOn = Convert.ToDateTime(loginAttemptsTable.Tables[0].Rows[i]["UpdatedOn"]);
                            objLoginAttempts.UpdatedIp = Convert.ToString(loginAttemptsTable.Tables[0].Rows[i]["UpdatedIp"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetLoginAttempts \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetLoginAttempts ", ex.Message + " \n " + ex.StackTrace);
            }
            return objLoginAttempts;
        }

        /// <summary>
        /// Method for updating login attempts
        /// </summary>
        /// <param name="loginAttempts"></param>
        /// <returns></returns>
        public bool UpdateLoginAttempts(LoginAttempts loginAttempts)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[4];

                SqlParameter objLoginAttempts = new SqlParameter("@LoginAttempts", SqlDbType.Int);
                objLoginAttempts.Value = loginAttempts.LoginAttempt;
                objLstParams[0] = objLoginAttempts;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = Convert.ToString(loginAttempts.UpdatedBy);
                objLstParams[1] = objUpdatedBy;

                SqlParameter objUpdatedOn = new SqlParameter("@UpdatedOn", SqlDbType.DateTime);
                objUpdatedOn.Value = loginAttempts.UpdatedOn;
                objLstParams[2] = objUpdatedOn;

                SqlParameter objUpdatedIp = new SqlParameter("@UpdatedIp", SqlDbType.VarChar);
                objUpdatedIp.Value = loginAttempts.UpdatedIp;
                objLstParams[3] = objUpdatedIp;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_UpdateLoginAttempts, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("UpdateLoginAttempts \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ UpdateLoginAttempts ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for clearing login attempts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ClearLoginAttempts(int userId)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[1];

                SqlParameter objuserId = new SqlParameter("@userId", SqlDbType.Int);
                objuserId.Value = userId;
                objLstParams[0] = objuserId;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_ClearLoginAttempts, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("ClearLoginAttempts \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ ClearLoginAttempts ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for getting user details by search cateria
        /// </summary>
        /// <param name="searchUser"></param>
        /// <returns></returns>
        public List<User> GetSearchUserDetailsByUserName(string searchUser)
        {
            List<User> objLstUser = new List<User>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                using (DataSet objUserDataSet = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetSearchUserDetailsByUserName, new SqlParameter("@SearchUser", searchUser)))
                {
                    if (objUserDataSet.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < objUserDataSet.Tables[0].Rows.Count; i++)
                        {
                            User objUser = new User();
                            UserLogin objUserLogin = new UserLogin();

                            objUser.Id = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UserId"]);
                            objUser.FirstName = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["FirstName"]);
                            objUser.LastName = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["LastName"]);
                            objUser.Mobile = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Mobile"]);
                            objUser.Gender = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["Gender"]);
                            objUser.Address = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Address"]);
                            objUser.CountryId = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["CountryId"]);
                            objUser.Email = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Email"]);
                            objUser.City = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["City"]);
                            objUser.ZipCode = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["ZipCode"]);
                            objUser.CreatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["CreatedBy"]);
                            objUser.CreatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["CreatedOn"]);
                            objUser.UpdatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UpdatedBy"]);
                            objUser.UpdatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["UpdatedOn"]);
                            objUser.IsEmailVerified = Convert.ToBoolean(objUserDataSet.Tables[0].Rows[i]["IsEmailVerified"]);
                            objUserLogin.UserName = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["UserName"]);
                            objUserLogin.Password = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Password"]);
                            objUserLogin.SecurityQuestion = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["SecurityQuestion"]);
                            objUserLogin.Answer = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["Answer"]);
                            objUserLogin.PasswordWrongAttempts = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["PasswordWrongAttempts"]);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["LastPasswordWrong"])))
                            {
                                objUserLogin.LastPasswordWrong = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["LastPasswordWrong"]);
                            }
                            else
                            {
                                objUserLogin.LastPasswordWrong = null;
                            }
                            objUserLogin.CreatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["CreatedBy"]);
                            objUserLogin.CreatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["CreatedOn"]);
                            objUserLogin.UpdatedBy = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UpdatedBy"]);
                            objUserLogin.UpdatedOn = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["UpdatedOn"]);
                            objUserLogin.RoleId = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["RoleId"]);
                            objUserLogin.IsActive = Convert.ToBoolean(objUserDataSet.Tables[0].Rows[i]["IsActive"]);
                            objUserLogin.UserId = Convert.ToInt32(objUserDataSet.Tables[0].Rows[i]["UserId"]);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["AccountLocked"])))
                            {
                                objUserLogin.AccountLocked = Convert.ToBoolean(objUserDataSet.Tables[0].Rows[i]["AccountLocked"]);
                            }
                            else
                            {
                                objUserLogin.AccountLocked = false;
                            }
                            objUser.UserLogin = objUserLogin;
                            objLstUser.Add(objUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetSearchUserDetailsByUserName \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetSearchUserDetailsByUserName ", ex.Message + " \n " + ex.StackTrace);
            }
            return objLstUser;
        }

        /// <summary>
        /// Method for logging login time
        /// </summary>
        /// <param name="objLoginLogoutHistory"></param>
        /// <returns></returns>
        public bool LogLoginTime(LoginHistory objLoginLogoutHistory)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[6];

                SqlParameter objuserId = new SqlParameter("@userId", SqlDbType.Int);
                objuserId.Value = objLoginLogoutHistory.UserId;
                objLstParams[0] = objuserId;

                SqlParameter objLoginTime = new SqlParameter("@LoginTime", SqlDbType.DateTime);
                objLoginTime.Value = objLoginLogoutHistory.LoginTime;
                objLstParams[1] = objLoginTime;

                SqlParameter objCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
                objCreatedBy.Value = objLoginLogoutHistory.CreatedBy;
                objLstParams[2] = objCreatedBy;

                SqlParameter objCreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                objCreatedOn.Value = objLoginLogoutHistory.CreatedOn;
                objLstParams[3] = objCreatedOn;

                SqlParameter objCreatedIp = new SqlParameter("@CreatedIp", SqlDbType.VarChar);
                objCreatedIp.Value = objLoginLogoutHistory.CreatedIp;
                objLstParams[4] = objCreatedIp;

                SqlParameter objUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
                objUserName.Value = objLoginLogoutHistory.UserName;
                objLstParams[5] = objUserName;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_LogLoginTime, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("LogLoginTime \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ LogLoginTime ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for logging logout time
        /// </summary>
        /// <param name="objLoginLogoutHistory"></param>
        /// <returns></returns>
        public bool LogLogoutTime(LoginHistory objLoginLogoutHistory)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[6];

                SqlParameter objuserId = new SqlParameter("@userId", SqlDbType.Int);
                objuserId.Value = objLoginLogoutHistory.UserId;
                objLstParams[0] = objuserId;

                SqlParameter objLogoutTime = new SqlParameter("@LogoutTime", SqlDbType.DateTime);
                objLogoutTime.Value = objLoginLogoutHistory.LogoutTime;
                objLstParams[1] = objLogoutTime;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = objLoginLogoutHistory.UpdatedBy;
                objLstParams[2] = objUpdatedBy;

                SqlParameter objUpdatedOn = new SqlParameter("@UpdatedOn", SqlDbType.DateTime);
                objUpdatedOn.Value = objLoginLogoutHistory.UpdatedOn;
                objLstParams[3] = objUpdatedOn;

                SqlParameter objUpdatedIp = new SqlParameter("@UpdatedIp", SqlDbType.NVarChar);
                objUpdatedIp.Value = objLoginLogoutHistory.UpdatedIp;
                objLstParams[4] = objUpdatedIp;

                SqlParameter objUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
                objUserName.Value = objLoginLogoutHistory.UserName;
                objLstParams[5] = objUserName;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_LogLogoutTime, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("LogLogoutTime \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ LogLogoutTime ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for getting login history
        /// </summary>
        /// <param name="searchUser"></param>
        /// <returns></returns>
        public List<LoginHistory> GetLoginHistory(string searchUser)
        {
            List<LoginHistory> objLstLoginHistory = new List<LoginHistory>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                using (DataSet objUserDataSet = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetLoginHistory, new SqlParameter("@SearchUser", searchUser == string.Empty ? null : searchUser)))
                {
                    if (objUserDataSet.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < objUserDataSet.Tables[0].Rows.Count; i++)
                        {
                            LoginHistory objLoginHistory = new LoginHistory();

                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["LoginTime"])))
                            {
                                objLoginHistory.LoginTime = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["LoginTime"]);
                            }

                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["LogoutTime"])))
                            {
                                objLoginHistory.LogoutTime = Convert.ToDateTime(objUserDataSet.Tables[0].Rows[i]["LogoutTime"]);
                            }
                            else
                            {
                                objLoginHistory.LogoutTime = null;
                            }

                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["CreatedIp"])))
                            {
                                objLoginHistory.CreatedIp = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["CreatedIp"]);
                            }
                            else
                            {
                                objLoginHistory.CreatedIp = null;
                            }

                            if (!string.IsNullOrWhiteSpace(Convert.ToString(objUserDataSet.Tables[0].Rows[i]["UpdatedIp"])))
                            {
                                objLoginHistory.UpdatedIp = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["UpdatedIp"]);
                            }
                            else
                            {
                                objLoginHistory.UpdatedIp = null;
                            }
                            objLoginHistory.UserName = Convert.ToString(objUserDataSet.Tables[0].Rows[i]["UserName"]);

                            objLstLoginHistory.Add(objLoginHistory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getLoginHistory \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ getLoginHistory ", ex.Message + " \n " + ex.StackTrace);
            }
            return objLstLoginHistory;
        }

        /// <summary>
        /// Method for updating user password details
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <returns></returns>
        public bool UpdateUserPassword(UserLogin objUserLogin)
        {
            bool result = false;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[4];

                SqlParameter objPassword = new SqlParameter("@Password", SqlDbType.VarChar);
                objPassword.Value = objUserLogin.Password;
                objLstParams[0] = objPassword;

                SqlParameter objUserId = new SqlParameter("@UserId", SqlDbType.Int);
                objUserId.Value = Convert.ToInt32(objUserLogin.UserId);
                objLstParams[1] = objUserId;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = Convert.ToInt32(objUserLogin.UpdatedBy);
                objLstParams[2] = objUpdatedBy;

                SqlParameter objUpdatedOn = new SqlParameter("@UpdatedOn", SqlDbType.DateTime);
                objUpdatedOn.Value = objUserLogin.UpdatedOn;
                objLstParams[3] = objUpdatedOn;

                result = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_UpdateUserPassword, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("UpdateUserPassword \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ UpdateUserPassword ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }
    }
}