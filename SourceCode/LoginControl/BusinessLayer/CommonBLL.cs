using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginControl.Model;
using LoginControl.DataAcessLayer;

namespace LoginControl.BusinessLayer
{
    public partial class CommonBLL
    {
        public CommonBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        CommonDAL objCommonDAL = new CommonDAL();

        /// <summary>
        /// Method for getting list of security questions.
        /// </summary>
        /// <returns></returns>
        public List<SecurityQuestion> GetAllSecurityQuestion()
        {
            return objCommonDAL.GetAllSecurityQuestion();
        }

        /// <summary>
        ///  Method for getting list of countries.
        /// </summary>
        /// <returns></returns>
        public List<Country> GetAllCountry()
        {
            return objCommonDAL.GetAllCountry();
        }

        /// <summary>
        ///  Method for getting list of roles.
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAllRoles()
        {
            return objCommonDAL.GetAllRoles();
        }

        /// <summary>
        ///  Method for adding audit log.
        /// </summary>
        /// <param name="objAuditLog"></param>
        /// <returns></returns>
        public int AddAuditLog(AuditLog objAuditLog)
        {
            return objCommonDAL.AddAuditLog(objAuditLog);
        }

        /// <summary>
        ///  Method for getting audit log details by Guid.
        /// </summary>
        /// <param name="GudId"></param>
        /// <returns></returns>
        public AuditLog GetAuditLogDetailsByGuid(string gudId)
        {
            return objCommonDAL.GetAuditLogDetailsByGuid(gudId);
        }

        /// <summary>
        ///  Method for adding email log.
        /// </summary>
        /// <param name="objEmailLog"></param>
        /// <returns></returns>
        public int AddEmailLog(EmailLog objEmailLog)
        {
            return objCommonDAL.AddEmailLog(objEmailLog);
        }
    }
}