using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.Model;
using LoginControl.BusinessLayer;
using LoginControl.Logs;
using log4net;

namespace LoginControl
{
    public partial class LoginLogoutHistory : System.Web.UI.Page
    {
        #region Global Varriables

        List<LoginHistory> objListLoginHistory = new List<LoginHistory>();
        UserBLL objUserBLL = new UserBLL();
        ILog log = LogManager.GetLogger("LockedUsers");
        const string PageName = "LockedUsers";

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BindLoginHistory(string.Empty);
                }
            }
            catch (Exception ex)
            {
                log.Error("Page_Load \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ Page_Load ", ex.Message + " \n " + ex.StackTrace);
            }

        }

        /// <summary>
        /// Method for binding login history for given user.
        /// </summary>
        /// <param name="userName">user name</param>
        private void BindLoginHistory(string userName)
        {
            try
            {
                objListLoginHistory = objUserBLL.GetLoginHistory(userName);
                grdLoginHistory.DataSource = objListLoginHistory;
                grdLoginHistory.DataBind();
            }
            catch (Exception ex)
            {
                log.Error("BindLoginHistory \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ BindLoginHistory ", ex.Message + " \n " + ex.StackTrace);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLoginHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLoginHistory.PageIndex = e.NewPageIndex;
            BindLoginHistory(txtUserName.Text.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoginHistory_Click(object sender, EventArgs e)
        {
            try
            {
                BindLoginHistory(txtUserName.Text.Trim());
            }
            catch (Exception ex)
            {
                log.Error("btnLoginHistory_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ btnLoginHistory_Click ", ex.Message + " \n " + ex.StackTrace);
            }
        }
    }
}