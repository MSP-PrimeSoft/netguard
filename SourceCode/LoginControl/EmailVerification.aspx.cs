using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.Model;
using LoginControl.BusinessLayer;
using log4net;
using LoginControl.Logs;

namespace LoginControl
{
    public partial class EmailVerification : System.Web.UI.Page
    {
        #region Global Varriables
        CommonBLL objCommonBLL = new CommonBLL();
        AuditLog objAuditLog = new AuditLog();
        UserBLL objUserBLL = new UserBLL();
        ILog log = LogManager.GetLogger("EmailVerification");
        const string PageName = "EmailVerification";

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Guid"]))
                {
                    string gudId = Convert.ToString(Request.QueryString["Guid"]);
                    objAuditLog = objCommonBLL.GetAuditLogDetailsByGuid(gudId);
                    if (objAuditLog != null)
                    {
                        if (objAuditLog.IsActive == true)
                        {
                            objUserBLL.UpdateUserStatus(objAuditLog, gudId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Page_Load \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ Page_Load ", ex.Message + " \n " + ex.StackTrace);
            }
        }
    }
}