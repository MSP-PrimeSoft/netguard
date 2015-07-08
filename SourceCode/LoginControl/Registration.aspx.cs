using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.Model;
using log4net;
using LoginControl.BusinessLayer;
using System.Configuration;
using System.Text.RegularExpressions;
using LoginControl.Logs;
using System.Drawing;
using LoginControl.Utilities;
using System.Net.Mail;

namespace LoginControl
{
    public partial class Registration : System.Web.UI.Page
    {
        #region Global Varaiables

        ILog log = LogManager.GetLogger("Registration");
        const string PageName = "Registration";

        CommonBLL objCommonBLL = new CommonBLL();
        UserBLL objUserBLL = new UserBLL();
        User objUser = new User();
        UserLogin objUserLogin = new UserLogin();
        int userId = 0;
        AuditLog objAuditLog = new AuditLog();
        EmailLog objEmailLog = new EmailLog();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BindDefaultValues();
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
        private void BindDefaultValues()
        {
            try
            {
                var Countries = objCommonBLL.GetAllCountry();
                if (Countries != null)
                {
                    ddlCountry.DataSource = Countries;
                    ddlCountry.DataValueField = "Id";
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataBind();
                }
                ddlCountry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));


                var Questions = objCommonBLL.GetAllSecurityQuestion();
                if (Questions != null)
                {
                    ddlSecurityQuestion.DataSource = Questions;
                    ddlSecurityQuestion.DataValueField = "Id";
                    ddlSecurityQuestion.DataTextField = "QuestionName";
                    ddlSecurityQuestion.DataBind();
                }
                ddlSecurityQuestion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

                var Roles = objCommonBLL.GetAllRoles();
                if (Roles != null)
                {
                    ddlRole.DataSource = Roles;
                    ddlRole.DataValueField = "Id";
                    ddlRole.DataTextField = "RoleName";
                    ddlRole.DataBind();
                }
                ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                log.Error("BindDefaultValues \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ BindDefaultValues ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ValidateUserName(object sender, ServerValidateEventArgs e)
        {
            try
            {
                var user = objUserBLL.GetUserDetailsByUserName(txtUserName.Text);
                if (user != null)
                {
                    if (user.Id > 0)
                    {
                        e.IsValid = false;
                        if (!e.IsValid)
                        {
                            cvUserName.IsValid = false;
                            cvUserName.ErrorMessage = "Already user exists with this User Name";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("ValidateUserName \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ ValidateUserName ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
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
                    if (Regex.IsMatch(txtPassword.Text.Trim(), @"[a-zA-Z]") && Regex.IsMatch(txtPassword.Text.Trim(), @"[0-9]"))
                    {
                        e.IsValid = true;
                    }
                    else
                    {
                        e.IsValid = false;
                        cvPassword.ErrorMessage = passwordError;
                    }
                }

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordIncludeSpecialCharacters"]) == true)
                {
                    Match match = Regex.Match(txtPassword.Text, @"[~`!@#$%^&*()-+=|\{}':;.,<>/?]");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    Page.Validate();
                    if (Page.IsValid)
                    {
                        objUser.FirstName = txtFirstName.Text.Trim();
                        objUser.LastName = txtLastName.Text.Trim();
                        objUser.Mobile = txtMobile.Text.Trim();
                        objUser.Email = txtEmail.Text.Trim();
                        objUser.Gender = Convert.ToInt32(rbnLstSex.SelectedValue.Trim());
                        objUser.Address = txtAddress.Text.Trim();
                        objUser.CountryId = Convert.ToInt32(ddlCountry.SelectedValue.Trim());
                        objUser.City = Convert.ToString(txtCity.Text.Trim());
                        objUser.ZipCode = Convert.ToString(txtZipCode.Text.Trim());
                        objUser.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        objUser.CreatedOn = DateTime.Now;
                        objUser.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        objUser.UpdatedOn = DateTime.Now;
                        objUser.IsEmailVerified = false;
                        objUserLogin.UserName = txtUserName.Text.Trim();
                        objUserLogin.Password = CommonUtils.Encrypt(txtPassword.Text.Trim());
                        objUserLogin.SecurityQuestion = Convert.ToInt32(ddlSecurityQuestion.SelectedValue.Trim());
                        objUserLogin.Answer = txtAnswer.Text.Trim();
                        objUserLogin.RoleId = Convert.ToInt32(ddlRole.SelectedValue.Trim());
                        objUserLogin.IsActive = false;
                        objUserLogin.AccountLocked = false;
                        objUser.UserLogin = objUserLogin;
                        userId = objUserBLL.AddUserDetails(objUser);
                        if (userId != 0)
                        {
                            SendVerficationEmail(userId);
                            lblMsg.Text = "User registered sucessfully. Check your registered email for confirmation email.";
                            lblMsg.ForeColor = Color.Green;
                            lblMsg.Font.Bold = true;
                            ClearAllFields(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("btnRegistration_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ btnRegistration_Click ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Method for clearing all fields.
        /// </summary>
        /// <param name="parent"></param>
        private void ClearAllFields(Control parent)
        {
            try
            {
                foreach (Control x in parent.Controls)
                {
                    if ((x.GetType() == typeof(TextBox)))
                    {

                        ((TextBox)(x)).Text = "";
                    }

                    if ((x.GetType() == typeof(DropDownList)))
                    {

                        ((DropDownList)(x)).SelectedValue = "0";
                    }

                    if (x.HasControls())
                    {
                        ClearAllFields(x);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("ClearAllFields \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ ClearAllFields ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Method for sends verification email.
        /// </summary>
        /// <param name="UserId"></param>
        private void SendVerficationEmail(int UserId)
        {
            try
            {
                // Set SMTP Server Settings
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = Convert.ToString(CommonUtils.SMTPServerName);
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Convert.ToString(CommonUtils.SMTPUserName), Convert.ToString(CommonUtils.SMTPPassword));
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                MailMessage objEmail = new MailMessage();
                objEmail.From = new MailAddress(Convert.ToString(CommonUtils.EmailVerificationFrom));
                objEmail.To.Add(new MailAddress(Convert.ToString(txtEmail.Text.Trim())));
                objEmail.Subject = Convert.ToString(CommonUtils.EmailVerificationSubject);
                objEmail.IsBodyHtml = true;
                objEmail.Body = Convert.ToString(CommonUtils.EmailVerificationBody);
                objEmail.IsBodyHtml = true;


                objAuditLog.UserId = UserId;
                objAuditLog.GudId = Guid.NewGuid();
                objAuditLog.CreatedBy = Convert.ToInt32(Session["UserId"]);
                objAuditLog.CreatedOn = DateTime.Now;
                objAuditLog.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                objAuditLog.UpdatedOn = DateTime.Now;
                objAuditLog.PageName = CommonUtils.EmailVerificationSucessPage;
                objAuditLog.IsActive = true;
                objCommonBLL.AddAuditLog(objAuditLog);

                objEmail.Body = objEmail.Body.Replace("[NAME]", txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim());
                objEmail.Body = objEmail.Body.Replace("[URL]", "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/" + CommonUtils.EmailLandingPage + "?Guid=" + objAuditLog.GudId);
                objEmail.Body = objEmail.Body.Replace("[UserName]", txtUserName.Text.Trim());

                objEmailLog.Subject = objEmail.Subject;
                objEmailLog.Body = objEmail.Body;
                objEmailLog.To = Convert.ToString(txtEmail.Text.Trim());
                objEmailLog.From = Convert.ToString(CommonUtils.EmailVerificationFrom);
                objEmailLog.CreatedBy = Convert.ToInt32(Session["UserId"]);
                objEmailLog.CreatedOn = DateTime.Now;
                objCommonBLL.AddEmailLog(objEmailLog);

                try
                {
                    client.Send(objEmail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                log.Error("SendEmailVerficationMail \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ SendEmailVerficationMail ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                imgCaptcha.ImageUrl = "~/CaptchaImage.ashx";
            }
            catch (Exception ex)
            {
                log.Error("imgRefresh_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ imgRefresh_Click ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Method for Validating Captcha.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
        {
            try
            {
                e.IsValid = false;
                if (!string.IsNullOrEmpty(txtCaptcha.Text) && !string.IsNullOrEmpty(Convert.ToString(Session["CaptchaImageText"])))
                {
                    if (Convert.ToString(Session["CaptchaImageText"]) == txtCaptcha.Text.Trim())
                    {
                        e.IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("ValidateCaptcha \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ ValidateCaptcha ", ex.Message + " \n " + ex.StackTrace);
            }

        }
    }
}