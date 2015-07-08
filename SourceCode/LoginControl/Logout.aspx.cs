using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using LoginControl.Model;
using LoginControl.BusinessLayer;
using LoginControl.Utilities;
using LoginControl.Logs;

namespace LoginControl
{
    public partial class Logout : System.Web.UI.Page
    {
        #region Global Variables
        ILog log = LogManager.GetLogger("Logout");
        const string PageName = "Logout";
        #endregion

        #region Page_Load functionality
        /// <summary>
        /// Page Load functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])) || !string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    LoginHistory objLoginLogoutHistory = new LoginHistory();
                    UserBLL objUserBLL = new UserBLL();

                    objLoginLogoutHistory.UserId = userId;
                    objLoginLogoutHistory.LogoutTime = DateTime.Now;
                    objLoginLogoutHistory.UpdatedBy = userId;
                    objLoginLogoutHistory.UpdatedOn = DateTime.Now;
                    objLoginLogoutHistory.UpdatedIp = CommonUtils.GetIPAddresses();
                    objLoginLogoutHistory.UserName = Convert.ToString(Session["UserName"]);

                    objUserBLL.LogLogoutTime(objLoginLogoutHistory);
                    ClearSession();
                    Response.Redirect("login.aspx");
                }
                Response.Redirect("login.aspx");
            }
            catch (Exception ex)
            {
                log.Error("Page_Load \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ Page_Load ", ex.Message + " \n " + ex.StackTrace);
                Response.Redirect("login.aspx");
            }

        }
        #endregion

        #region Clear Session
        /// <summary>
        /// Clears the Current Session
        /// </summary>
        private void ClearSession()
        {
            Session["LoginId"] = "";
            Session["RoleId"] = "";
            Session.Abandon();
        }
        #endregion
    }
}