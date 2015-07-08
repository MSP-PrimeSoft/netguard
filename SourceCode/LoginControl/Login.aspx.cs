using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using LoginControl.BusinessLayer;
using LoginControl.Model;
using System.Net;
using LoginControl.Logs;
using LoginControl.Utilities;
using System.DirectoryServices.AccountManagement;

namespace LoginControl
{
    public partial class Login : System.Web.UI.Page
    {
        #region Global Varriables

        User objUser = new User();
        UserBLL objUserBLL = new UserBLL();
        LoginAttempts objLoginAttempts = new LoginAttempts();
        UserLogin objUserLogin = new UserLogin();
        LoginHistory objLoginHistory = new LoginHistory();
        ILog log = LogManager.GetLogger("_Default");
        const string PageName = "_Default";

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
                    if (Request.Cookies["UserName"] != null)
                        txtUserName.Text = Request.Cookies["UserName"].Value;
                    if (Request.Cookies["Password"] != null)
                        txtPassword.Attributes.Add("value", Request.Cookies["Password"].Value.ToString());
                    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    {
                        chkRememberPassword.Checked = true;
                    }
                    else
                    {
                        chkRememberPassword.Checked = false;
                    }
                }
                lblError.Attributes.Add("display", "none");
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Active Directory selected
                if (rdolstAuthenticationType.SelectedValue == "0")
                {
                    Dns.GetHostName();
                    ExceptionLog.WriteLog(PageName + " @ HttpContext.Current.User.Identity.Name ", HttpContext.Current.User.Identity.Name + " \n ");
                    ExceptionLog.WriteLog(PageName + " @  Dns.GetHostName()", Dns.GetHostName() + " \n ");
                    ExceptionLog.WriteLog(PageName + " @  Dns.GetHostEntry(Request.ServerVariables[]).HostName", Dns.GetHostEntry(Dns.GetHostName()).HostName + " \n ");
                    ExceptionLog.WriteLog(PageName + " @  System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName", System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName + " \n ");
                    if (DoesUserExist(System.Security.Principal.WindowsIdentity.GetCurrent().Name))
                    {
                        objLoginHistory.UserId = Convert.ToInt32(Session["UserId"]);
                        objLoginHistory.LoginTime = DateTime.Now;
                        objLoginHistory.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        objLoginHistory.CreatedOn = DateTime.Now;
                        objLoginHistory.CreatedIp = CommonUtils.GetIPAddresses();
                        objLoginHistory.UserName = Environment.UserName;
                        objUserBLL.LogLoginTime(objLoginHistory);

                        Session["UserName"] = Environment.UserName;
                        Response.Redirect("LoginLogoutHistory.aspx");
                    }
                    else
                    {
                        lblError.Text = "No account found with that username.";
                        lblError.Attributes.Add("display", "block");
                        txtUserName.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                    }
                }
                else if (rdolstAuthenticationType.SelectedValue == "1")
                {
                    if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        //SQL Server Validation
                        objUser = objUserBLL.GetUserDetailsByUserName(txtUserName.Text.Trim());
                        if (objUser != null)
                        {
                            if (objUser.Id != 0 && objUser.UserLogin.IsActive == true)
                            {
                                if (objUser.UserLogin.AccountLocked == true)
                                {
                                    lblError.Text = "The account is locked please contact administrator.";
                                    lblError.Attributes.Add("display", "block");
                                    txtUserName.Text = string.Empty;
                                    txtPassword.Text = string.Empty;
                                }
                                else
                                {
                                    if (objUser.UserLogin.Password.Equals(CommonUtils.Encrypt(txtPassword.Text.Trim())))
                                    {
                                        Session["RoleId"] = Convert.ToString(objUser.UserLogin.RoleId);
                                        Session["UserId"] = Convert.ToString(objUser.Id);
                                        objLoginHistory.UserId = objUser.Id;
                                        objLoginHistory.LoginTime = DateTime.Now;
                                        objLoginHistory.CreatedBy = Convert.ToInt32(Session["UserId"]);
                                        objLoginHistory.CreatedOn = DateTime.Now;
                                        objLoginHistory.CreatedIp = CommonUtils.GetIPAddresses();
                                        objLoginHistory.UserName = objUser.UserLogin.UserName;
                                        objUserBLL.LogLoginTime(objLoginHistory);
                                        objUserBLL.ClearLoginAttempts(objUser.Id);

                                        if (chkRememberPassword.Checked == true)
                                        {
                                            Response.Cookies["UserName"].Value = txtUserName.Text;
                                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(10);
                                            Response.Cookies["Password"].Value = txtPassword.Text;
                                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(10);
                                        }
                                        else
                                        {
                                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                                        }
                                        Response.Redirect("LoginLogoutHistory.aspx");
                                    }
                                    else
                                    {
                                        objLoginAttempts = objUserBLL.GetLoginAttempts();
                                        if (objLoginAttempts != null && objLoginAttempts.Id > 0)
                                        {
                                            if (objLoginAttempts.LoginAttempt > objUser.UserLogin.PasswordWrongAttempts)
                                            {
                                                if (objLoginAttempts.LoginAttempt == objUser.UserLogin.PasswordWrongAttempts + 1)
                                                {
                                                    objUserLogin.AccountLocked = true;
                                                    objUserLogin.PasswordWrongAttempts = objUser.UserLogin.PasswordWrongAttempts + 1;
                                                    objUserLogin.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                                                    objUserLogin.UpdatedOn = DateTime.Now;
                                                    objUserLogin.LastPasswordWrong = DateTime.Now;
                                                    objUserLogin.UserId = objUser.Id;
                                                    objUserBLL.LockUserDetails(objUserLogin);
                                                }
                                                else
                                                {
                                                    objUserLogin.AccountLocked = false;
                                                    objUserLogin.PasswordWrongAttempts = objUser.UserLogin.PasswordWrongAttempts + 1;
                                                    objUserLogin.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                                                    objUserLogin.UpdatedOn = DateTime.Now;
                                                    objUserLogin.LastPasswordWrong = DateTime.Now;
                                                    objUserLogin.UserId = objUser.Id;
                                                    objUserBLL.UpdatePasswordWorngAttemptDetails(objUserLogin);

                                                    lblError.Text = "Please enter correct User name and password Your access will be locked after " + (objLoginAttempts.LoginAttempt - (objUser.UserLogin.PasswordWrongAttempts + 1)) + " consecutive wrong attempts.";
                                                    lblError.Attributes.Add("display", "block");
                                                    txtUserName.Text = string.Empty;
                                                    txtPassword.Text = string.Empty;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            lblError.Text = "The username or password you entered is incorrect.";
                                            lblError.Attributes.Add("display", "block");
                                            txtUserName.Text = string.Empty;
                                            txtPassword.Text = string.Empty;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lblError.Text = "No account found with that username.";
                                lblError.Attributes.Add("display", "block");
                                txtUserName.Text = string.Empty;
                                txtPassword.Text = string.Empty;
                            }
                        }
                        else
                        {
                            lblError.Text = "No account found with that username.";
                            lblError.Attributes.Add("display", "block");
                            txtUserName.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("btnLogin_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ btnLogin_Click ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool DoesUserExist(string userName)
        {
            ExceptionLog.WriteLog(PageName + " @ Environment.UserDomainName", Environment.UserDomainName + " \n ");
            ExceptionLog.WriteLog(PageName + " @ userName", userName + " \n ");

            using (var domainContext = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName))
                {
                    return foundUser != null;
                }
            }
        }
    }
}