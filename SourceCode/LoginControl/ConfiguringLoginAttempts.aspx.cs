using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.BusinessLayer;
using log4net;
using LoginControl.Model;
using LoginControl.Logs;
using LoginControl.Utilities;
using System.Drawing;

namespace LoginControl
{
    public partial class ConfiguringLoginAttempts : System.Web.UI.Page
    {
        #region Global Varriables
        UserBLL objUserBLL = new UserBLL();
        LoginAttempts objLoginAttempts = new LoginAttempts();
        ILog log = LogManager.GetLogger("ConfiguringLoginAttempts");
        const string PageName = "ConfiguringLoginAttempts";

        #endregion

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    objLoginAttempts = objUserBLL.GetLoginAttempts();
                    txtLoginAttempts.Text = "0";
                    if (objLoginAttempts != null)
                    {
                        if (objLoginAttempts.Id > 0)
                        {
                            txtLoginAttempts.Text = Convert.ToString(objLoginAttempts.LoginAttempt);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLoginAttempts.Text))
                {
                    objLoginAttempts.LoginAttempt = Convert.ToInt32(txtLoginAttempts.Text.Trim());
                    objLoginAttempts.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                    objLoginAttempts.UpdatedOn = DateTime.Now;
                    objLoginAttempts.UpdatedIp = Convert.ToString(CommonUtils.GetIPAddresses());
                    objUserBLL.UpdateLoginAttempts(objLoginAttempts);
                    lblMsg.Visible = true;
                    lblMsg.Text = "Login attempts updated sucessfully.";
                    lblMsg.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                log.Error("btnSave_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ btnSave_Click ", ex.Message + " \n " + ex.StackTrace);
            }
        }
    }
}