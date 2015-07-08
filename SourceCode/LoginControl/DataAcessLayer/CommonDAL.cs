using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginControl.Model;
using System.Data;
using log4net;
using System.Data.SqlClient;
using LoginControl.Utilities;
using LoginControl.Logs;

namespace LoginControl.DataAcessLayer
{
    public partial class CommonDAL
    {

        #region Stored Procedure Names

        ILog log = LogManager.GetLogger("CommonDAL");
        const string COMMONDATA = "CommonDAL";
        private const string SP_GetAllSecurityQuestion = "usp_GetAllSecurityQuestion";
        private const string SP_GetAllCountry = "usp_GetAllCountry";
        private const string SP_GetAllRoles = "usp_GetAllRoles";
        private const string SP_AddAuditLog = "usp_AddAuditLog";
        private const string SP_GetAuditLogDetailsByGuid = "usp_GetAuditLogDetailsByGuid";
        private const string SP_AddEmailLog = "usp_AddEmailLog";
        private const string SP_GetPasswordPolicy = "usp_GetPasswordPolicy";

        #endregion

        public CommonDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Method for getting all security questions details
        /// </summary>
        /// <returns></returns>
        public List<SecurityQuestion> GetAllSecurityQuestion()
        {
            List<SecurityQuestion> objLstSecurityQuestion = new List<SecurityQuestion>();
            try
            {
                using (DataSet SecurityQuestionTable = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetAllSecurityQuestion))
                {
                    if (SecurityQuestionTable.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < SecurityQuestionTable.Tables[0].Rows.Count; i++)
                        {
                            SecurityQuestion objSecurityQuestion = new SecurityQuestion();
                            objSecurityQuestion.Id = Convert.ToInt32(SecurityQuestionTable.Tables[0].Rows[i]["Id"]);
                            objSecurityQuestion.QuestionName = Convert.ToString(SecurityQuestionTable.Tables[0].Rows[i]["QuestionName"]);
                            objSecurityQuestion.UpdatedBy = Convert.ToInt32(SecurityQuestionTable.Tables[0].Rows[i]["UpdatedBy"]);
                            objSecurityQuestion.UpdatedOn = Convert.ToDateTime(SecurityQuestionTable.Tables[0].Rows[i]["UpdatedOn"]);
                            objLstSecurityQuestion.Add(objSecurityQuestion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetAllSecurityQuestion \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetAllSecurityQuestion ", ex.Message + " \n " + ex.StackTrace);
            }
            return objLstSecurityQuestion;
        }

        /// <summary>
        /// Method for getting all country details
        /// </summary>
        /// <returns></returns>
        public List<Country> GetAllCountry()
        {
            List<Country> objLstCountry = new List<Country>();
            try
            {
                using (DataSet SecurityQuestionTable = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetAllCountry))
                {
                    if (SecurityQuestionTable.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < SecurityQuestionTable.Tables[0].Rows.Count; i++)
                        {
                            Country objCountry = new Country();
                            objCountry.Id = Convert.ToInt32(SecurityQuestionTable.Tables[0].Rows[i]["Id"]);
                            objCountry.CountryName = Convert.ToString(SecurityQuestionTable.Tables[0].Rows[i]["CountryName"]);
                            objCountry.CreatedBy = Convert.ToInt32(SecurityQuestionTable.Tables[0].Rows[i]["CreatedBy"]);
                            objCountry.CreatedOn = Convert.ToDateTime(SecurityQuestionTable.Tables[0].Rows[i]["CreatedOn"]);
                            objCountry.UpdatedBy = Convert.ToInt32(SecurityQuestionTable.Tables[0].Rows[i]["UpdatedBy"]);
                            objCountry.UpdatedOn = Convert.ToDateTime(SecurityQuestionTable.Tables[0].Rows[i]["UpdatedOn"]);
                            objLstCountry.Add(objCountry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetAllCountry \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetAllCountry ", ex.Message + " \n " + ex.StackTrace);
            }
            return objLstCountry;
        }

        /// <summary>
        ///  Method for getting all role
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAllRoles()
        {
            List<Role> objLstRoles = new List<Role>();
            try
            {
                using (DataSet RoleTable = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetAllRoles))
                {
                    if (RoleTable.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < RoleTable.Tables[0].Rows.Count; i++)
                        {
                            Role objRoles = new Role();
                            objRoles.Id = Convert.ToInt32(RoleTable.Tables[0].Rows[i]["Id"]);
                            objRoles.RoleName = Convert.ToString(RoleTable.Tables[0].Rows[i]["RoleName"]);
                            objRoles.IsActive = Convert.ToBoolean(RoleTable.Tables[0].Rows[i]["IsActive"]);
                            objLstRoles.Add(objRoles);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetAllRoles \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ GetAllRoles ", ex.Message + " \n " + ex.StackTrace);
            }
            return objLstRoles;
        }

        /// <summary>
        /// Method for adding audit details
        /// </summary>
        /// <param name="objAuditLog"></param>
        /// <returns></returns>
        public int AddAuditLog(AuditLog objAuditLog)
        {
            int result = 0;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[8];

                SqlParameter objUserId = new SqlParameter("@UserId", SqlDbType.Int);
                objUserId.Value = objAuditLog.UserId;
                objLstParams[0] = objUserId;

                SqlParameter objGudId = new SqlParameter("@GudId", SqlDbType.VarChar);
                objGudId.Value = Convert.ToString(objAuditLog.GudId);
                objLstParams[1] = objGudId;

                SqlParameter objCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
                objCreatedBy.Value = objAuditLog.CreatedBy;
                objLstParams[2] = objCreatedBy;

                SqlParameter objCreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                objCreatedOn.Value = objAuditLog.CreatedOn;
                objLstParams[3] = objCreatedOn;

                SqlParameter objUpdatedBy = new SqlParameter("@UpdatedBy", SqlDbType.Int);
                objUpdatedBy.Value = objAuditLog.UpdatedBy;
                objLstParams[4] = objUpdatedBy;

                SqlParameter objUpdatedOn = new SqlParameter("@UpdatedOn", SqlDbType.DateTime);
                objUpdatedOn.Value = objAuditLog.UpdatedOn;
                objLstParams[5] = objUpdatedOn;

                SqlParameter objPageName = new SqlParameter("@PageName", SqlDbType.VarChar);
                objPageName.Value = objAuditLog.PageName;
                objLstParams[6] = objPageName;

                SqlParameter objIsActive = new SqlParameter("@IsActive", SqlDbType.Bit);
                objIsActive.Value = objAuditLog.IsActive;
                objLstParams[7] = objIsActive;

                result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_AddAuditLog, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("AddAuditLog \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ AddAuditLog ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Method for Getting audit log details
        /// </summary>
        /// <param name="GudId"></param>
        /// <returns></returns>
        public AuditLog GetAuditLogDetailsByGuid(string gudId)
        {
            AuditLog objAuditLog = new AuditLog();
            try
            {
                SqlParameter objGudId = new SqlParameter("@GudId", SqlDbType.VarChar);
                objGudId.Value = gudId;

                using (DataSet AuditLogTable = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_GetAuditLogDetailsByGuid, objGudId))
                {
                    if (AuditLogTable.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < AuditLogTable.Tables[0].Rows.Count; i++)
                        {
                            objAuditLog.Id = Convert.ToInt32(AuditLogTable.Tables[0].Rows[i]["Id"]);
                            objAuditLog.UserId = Convert.ToInt32(AuditLogTable.Tables[0].Rows[i]["UserId"]);
                            //objAuditLog.GudId = Convert.ToString(AuditLogTable.Tables[0].Rows[i]["GudId"]);
                            objAuditLog.CreatedBy = Convert.ToInt32(AuditLogTable.Tables[0].Rows[i]["CreatedBy"]);
                            objAuditLog.CreatedOn = Convert.ToDateTime(AuditLogTable.Tables[0].Rows[i]["CreatedOn"]);
                            objAuditLog.UpdatedBy = Convert.ToInt32(AuditLogTable.Tables[0].Rows[i]["UpdatedBy"]);
                            objAuditLog.UpdatedOn = Convert.ToDateTime(AuditLogTable.Tables[0].Rows[i]["UpdatedOn"]);
                            objAuditLog.PageName = Convert.ToString(AuditLogTable.Tables[0].Rows[i]["PageName"]);
                            objAuditLog.IsActive = Convert.ToBoolean(AuditLogTable.Tables[0].Rows[i]["IsActive"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getAuditLogDetailsByGuid \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ getAuditLogDetailsByGuid ", ex.Message + " \n " + ex.StackTrace);
            }
            return objAuditLog;
        }

        /// <summary>
        /// Method for adding email details in to email log
        /// </summary>
        /// <param name="objEmailLog"></param>
        /// <returns></returns>
        public int AddEmailLog(EmailLog objEmailLog)
        {
            int result = 0;
            try
            {
                SqlCommand objSqlCommand = new SqlCommand();

                SqlParameter[] objLstParams = new SqlParameter[6];

                SqlParameter objSubject = new SqlParameter("@Subject", SqlDbType.VarChar);
                objSubject.Value = objEmailLog.Subject;
                objLstParams[0] = objSubject;

                SqlParameter objBody = new SqlParameter("@Body", SqlDbType.VarChar);
                objBody.Value = Convert.ToString(objEmailLog.Body);
                objLstParams[1] = objBody;

                SqlParameter objTo = new SqlParameter("@To", SqlDbType.VarChar);
                objTo.Value = objEmailLog.To;
                objLstParams[2] = objTo;

                SqlParameter objFrom = new SqlParameter("@From", SqlDbType.VarChar);
                objFrom.Value = objEmailLog.From;
                objLstParams[3] = objFrom;

                SqlParameter objCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
                objCreatedBy.Value = objEmailLog.CreatedBy;
                objLstParams[4] = objCreatedBy;

                SqlParameter objCreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                objCreatedOn.Value = objEmailLog.CreatedOn;
                objLstParams[5] = objCreatedOn;

                result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, SP_AddEmailLog, objLstParams));
                objSqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                log.Error("AddEmailLog \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(COMMONDATA + " @ AddEmailLog ", ex.Message + " \n " + ex.StackTrace);
            }
            return result;
        }
    }
}