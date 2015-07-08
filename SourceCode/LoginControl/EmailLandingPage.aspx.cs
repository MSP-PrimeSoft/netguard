using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.BusinessLayer;
using LoginControl.Model;
using log4net;
using LoginControl.Logs;

namespace LoginControl
{
    public partial class EmailLandingPage : System.Web.UI.Page
    {
        
        #region Global Varriables
        string gudId = string.Empty;
        CommonBLL objCommonBLL = new CommonBLL();
        AuditLog objAuditLog = new AuditLog();
        ILog log = LogManager.GetLogger("EmailLandingPage");
        const string PageName = "EmailLandingPage";

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Guid"]))
                    {
                        gudId = Convert.ToString(Request.QueryString["Guid"]);
                        objAuditLog = objCommonBLL.GetAuditLogDetailsByGuid(gudId);
                        if (objAuditLog != null)
                        {
                            if (objAuditLog.IsActive == true)
                            {
                                Response.Redirect(objAuditLog.PageName + "?Guid=" + gudId);
                            }
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