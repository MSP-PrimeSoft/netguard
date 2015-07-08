using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using LoginControl.Utilities;
using System.Drawing;
using LoginControl.BusinessLayer;
using LoginControl.Model;
using System.Configuration;
using LoginControl.Logs;
using System.Text.RegularExpressions;

namespace LoginControl
{
    public partial class UserChangePassword : System.Web.UI.Page
    {
        #region Global Varriables

        ILog log = LogManager.GetLogger("UserChangePassword");
        const string PageName = "UserChangePassword";
        UserBLL objUserBLL = new UserBLL();
        UserLogin objUserLogin = new UserLogin();
        int userId = 0;
        public int passwordMinLength = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordMinLength"]);
        public int passwordMaxLength = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordMaxLength"]);
        public bool passwordCaseSensitivity = Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordCaseSensitivity"]);
        public bool passwordIncludeNumerical = Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeNumerical"]);
        public bool passwordIncludeSpecialCharacters = Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeSpecialCharacters"]);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPassword.Text.Trim()) && !string.IsNullOrEmpty(txtNewPassword.Text.Trim()) && !string.IsNullOrEmpty(txtNewConfirmPassword.Text.Trim()))
                {
                    userId = Convert.ToInt32(Session["UserId"]);
                    User objUser = objUserBLL.GetUserDetailsById(userId);
                    if (objUser.Id != 0)
                    {
                        if (CommonUtils.Encrypt(txtPassword.Text).Equals(objUser.UserLogin.Password))
                        {
                            objUserLogin.Password = CommonUtils.Encrypt(txtNewPassword.Text);
                            objUserLogin.UpdatedBy = userId;
                            objUserLogin.UserId = userId;
                            objUserLogin.UpdatedOn = DateTime.Now;
                            objUserBLL.UpdateUserPassword(objUserLogin);
                            lblError.Text = "Password updated successfully.";
                            lblError.Visible = true;
                            cvNewPassword.Visible = false;
                            lblError.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblError.ForeColor = Color.Red;
                            lblError.Text = "Password does not match.";
                            lblError.Visible = true;
                            cvNewPassword.Visible = true;
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
        /// Method for validating password.
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
                    cvNewPassword.ErrorMessage = passwordError;

                }

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordCaseSensitivity"]) == true)
                {
                    if (txtPassword.Text.ToLower() == txtPassword.Text || txtPassword.Text.ToUpper() == txtPassword.Text)
                    {
                        e.IsValid = false;
                        cvNewPassword.ErrorMessage = passwordError;
                    }
                }

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeNumerical"]) == true)
                {
                    Regex r = new Regex("^[a-zA-Z0-9]*$");
                    if (!r.IsMatch(txtPassword.Text))
                    {
                        e.IsValid = false;
                        cvNewPassword.ErrorMessage = passwordError;
                    }
                }

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeSpecialCharacters"]) == true)
                {
                    Match match = Regex.Match(txtPassword.Text, "[^a-z0-9]", RegexOptions.IgnoreCase);
                    if (!match.Success)
                    {
                        e.IsValid = false;
                        cvNewPassword.ErrorMessage = passwordError;
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