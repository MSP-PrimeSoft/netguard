using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.Utilities;
using System.Net.Mail;
using LoginControl.BusinessLayer;
using LoginControl.Model;
using log4net;
using LoginControl.Logs;

namespace LoginControl
{
    public partial class ForgetPassword : System.Web.UI.Page
    {

        #region Global Varriables

        UserBLL objUserBLL = new UserBLL();
        User objUser = new User();
        CommonBLL objCommonBLL = new CommonBLL();
        AuditLog objAuditLog = new AuditLog();
        EmailLog objEmailLog = new EmailLog();
        ILog log = LogManager.GetLogger("EmailVerification");
        const string PageName = "EmailVerification";

        #endregion


        /// <summary>
        /// Used for sending email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    objUser = objUserBLL.GetUserDetailsByEmailId(txtEmail.Text.Trim());
                    if (objUser != null)
                    {
                        if (objUser.Id != 0)
                        {
                            if (objUser.IsEmailVerified == true)
                            {
                                SendForgetPassword(objUser.Id, objUser.FirstName, objUser.LastName, objUser.UserLogin.UserName);
                            }
                            else
                            {
                                lblErrorMsg.Text = "Email address is not verified, Please click the link sent to your registered email address.";
                            }
                        }
                        else
                        {
                            lblErrorMsg.Text = "No account found with that email address.";
                        }
                    }
                    else
                    {
                        lblErrorMsg.Text = "No account found with that email address.";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("btnSendMail_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ btnSendMail_Click ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        private void SendForgetPassword(int userId, string fName, string lName, string userName)
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
                objEmail.Subject = Convert.ToString(CommonUtils.ForgotPasswordSubject);
                objEmail.IsBodyHtml = true;
                objEmail.Body = Convert.ToString(CommonUtils.ForgotPasswordBody);
                objEmail.IsBodyHtml = true;

                objAuditLog.UserId = userId;
                objAuditLog.GudId = Guid.NewGuid();
                objAuditLog.CreatedBy = Convert.ToInt32(Session["UserId"]);
                objAuditLog.CreatedOn = DateTime.Now;
                objAuditLog.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                objAuditLog.UpdatedOn = DateTime.Now;
                objAuditLog.PageName = CommonUtils.ChangePasswordPage;
                objAuditLog.IsActive = true;
                objCommonBLL.AddAuditLog(objAuditLog);

                objEmail.Body = objEmail.Body.Replace("[NAME]", fName.Trim() + " " + lName.Trim());
                objEmail.Body = objEmail.Body.Replace("[URL]", "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/" + CommonUtils.EmailLandingPage + "?Guid=" + objAuditLog.GudId);
                objEmail.Body = objEmail.Body.Replace("[UserName]", userName);

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
                catch (Exception)
                {

                }
                lblErrorMsg.Text = "Please click the link sent to your registered email address.";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Please click the link sent to your registered email address.";
                log.Error("SendForgetPassword \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ SendForgetPassword ", ex.Message + " \n " + ex.StackTrace);
            }
        }
    }
}