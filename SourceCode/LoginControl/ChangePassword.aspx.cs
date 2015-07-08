using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.BusinessLayer;
using LoginControl.Model;
using System.Configuration;
using log4net;
using LoginControl.Utilities;
using LoginControl.Logs;
using System.Text.RegularExpressions;

namespace LoginControl
{
    public partial class ChangePassword : System.Web.UI.Page
    { 
     
    #region Global Varriables
        string GudId = string.Empty;
        CommonBLL objCommonBLL = new CommonBLL();
        AuditLog objAuditLog = new AuditLog();
        UserBLL objUserBLL = new UserBLL();
        UserLogin objUserLogin = new UserLogin();
        public int passwordMinLength = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordMinLength"]);
        public int passwordMaxLength = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordMaxLength"]);
        public bool passwordCaseSensitivity = Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordCaseSensitivity"]);
        public bool passwordIncludeNumerical = Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeNumerical"]);
        public bool passwordIncludeSpecialCharacters = Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeSpecialCharacters"]);
        ILog log = LogManager.GetLogger("ChangePassword");
        const string PageName = "ChangePassword";
    #endregion
    
    /// <summary>
    /// Method for changing password.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Guid"]))
            {
                GudId = Convert.ToString(Request.QueryString["Guid"]);
                objAuditLog = objCommonBLL.GetAuditLogDetailsByGuid(GudId);
                if (objAuditLog != null)
                {
                    if (objAuditLog.IsActive == true)
                    {
                        objUserLogin.UserId = objAuditLog.UserId;
                        objUserLogin.Password = CommonUtils.Encrypt(txtPassword.Text.Trim());
                        objUserLogin.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        objUserLogin.UpdatedOn = DateTime.Now;
                        objUserBLL.UpdatePassword(objUserLogin, GudId);
                        Response.Redirect("Login.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("btnChangePassword_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
            ExceptionLog.WriteLog(PageName + " @ btnChangePassword_Click ", ex.Message + " \n " + ex.StackTrace);
        }
    }

    /// <summary>
    /// method for validating password.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ValidatePassword(object sender, ServerValidateEventArgs e)
    {
        try
        {
            string passwordError = "Password must be mininum " + Convert.ToString(ConfigurationManager.AppSettings["PasswordMinLength"]) + " character length, maximum " + Convert.ToString(ConfigurationManager.AppSettings["PasswordMaxLength"]) + " character length";

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordCaseSensitivity"]) == true)
            {
                passwordError += ", a lowercase letter or an uppercase letter";
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeNumerical"]) == true)
            {
                passwordError += ", an integer";

            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeSpecialCharacters"]) == true)
            {
                passwordError += ", a special character";
            }

            if (txtPassword.Text.Length < passwordMinLength && txtPassword.Text.Length > passwordMaxLength)
            {
                e.IsValid = false;
                cvPassword.ErrorMessage = passwordError;

            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordCaseSensitivity"]) == true)
            {
                if (txtPassword.Text.ToLower() == txtPassword.Text || txtPassword.Text.ToUpper() == txtPassword.Text)
                {
                    e.IsValid = false;
                    cvPassword.ErrorMessage = passwordError;
                }
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeNumerical"]) == true)
            {
                Regex r = new Regex("^[a-zA-Z0-9]*$");
                if (!r.IsMatch(txtPassword.Text))
                {
                    e.IsValid = false;
                    cvPassword.ErrorMessage = passwordError;
                }
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeSpecialCharacters"]) == true)
            {
                Match match = Regex.Match(txtPassword.Text, "[^a-z0-9]", RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    e.IsValid = false;
                    cvPassword.ErrorMessage = passwordError;
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("ValidatePassword \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
            ExceptionLog.WriteLog(PageName + " @ ValidatePassword ", ex.Message + " \n " + ex.StackTrace);
        }
    }
    }
}