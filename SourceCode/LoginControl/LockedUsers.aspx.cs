using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using LoginControl.Model;
using LoginControl.BusinessLayer;
using LoginControl.Logs;

namespace LoginControl
{
    public partial class LockedUsers : System.Web.UI.Page
    {
        #region Global Varriables

        List<User> objListUsers = new List<User>();
        UserBLL objUserBLL = new UserBLL();
        LoginAttempts objLoginAttempts = new LoginAttempts();
        UserLogin objUserLogin = new UserLogin();
        int userId = 0;
        ILog log = LogManager.GetLogger("LockedUsers");
        const string PageName = "LockedUsers";

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
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                    {
                        userId = Convert.ToInt32(Session["UserId"]);
                    }
                    BindUsers(string.Empty);
                }
            }
            catch (Exception ex)
            {
                log.Error("Page_Load \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ Page_Load ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Method for binding users to grid.
        /// </summary>
        /// <param name="searchUser"></param>
        private void BindUsers(string searchUser)
        {
            try
            {
                grdLockedUsers.ShowFooter = true;
                if (string.IsNullOrEmpty(searchUser))
                {
                    objListUsers = objUserBLL.GetUserDetails();
                }
                else
                {
                    // getting users based on search condition
                    objListUsers = objUserBLL.GetSearchUserDetailsByUserName(searchUser);
                }

                if (objListUsers != null)
                {
                    grdLockedUsers.DataSource = objListUsers;
                    grdLockedUsers.DataBind();
                    if (objListUsers.Count > 10)
                    {
                        grdLockedUsers.ShowFooter = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("BindUsers \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ BindUsers ", ex.Message + " \n " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLockedUsers_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtUserName.Text.Trim());
            }
            catch (Exception ex)
            {
                log.Error("btnLockedUsers_Click \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ btnLockedUsers_Click ", ex.Message + " \n " + ex.StackTrace);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLockedUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLockedUsers.PageIndex = e.NewPageIndex;
            BindUsers(txtUserName.Text.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLockedUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Locked")
                {
                    ImageButton lnkBtn = (ImageButton)e.CommandSource;
                    GridViewRow grdCurrentRow = (GridViewRow)lnkBtn.Parent.Parent;
                    GridView myGrid = (GridView)sender;
                    int ID = Convert.ToInt32(myGrid.DataKeys[grdCurrentRow.RowIndex].Value.ToString());
                    if (Convert.ToBoolean(e.CommandArgument))
                    {
                        Int32.TryParse(Convert.ToString(Session["UserId"]), out userId);
                        User objUser = objUserBLL.GetUserDetailsById(userId);
                        objUserLogin.AccountLocked = false;
                        objUserLogin.PasswordWrongAttempts = 0;
                        objUserLogin.UpdatedBy = objUser.Id;
                        objUserLogin.UpdatedOn = DateTime.Now;
                        objUserLogin.LastPasswordWrong = null;
                        objUserLogin.UserId = ID;
                        objUserBLL.UpdatePasswordWorngAttemptDetails(objUserLogin);
                    }
                    else
                    {
                        objLoginAttempts = objUserBLL.GetLoginAttempts();
                        if (objLoginAttempts != null && objLoginAttempts.Id > 0)
                        {
                            Int32.TryParse(Convert.ToString(Session["UserId"]), out userId);
                            User objUser = objUserBLL.GetUserDetailsById(userId);
                            objUserLogin.AccountLocked = true;
                            objUserLogin.PasswordWrongAttempts = objLoginAttempts.LoginAttempt;
                            objUserLogin.UpdatedBy = objUser.Id;
                            objUserLogin.UpdatedOn = DateTime.Now;
                            objUserLogin.LastPasswordWrong = DateTime.Now;
                            objUserLogin.UserId = ID;
                            objUserBLL.UpdatePasswordWorngAttemptDetails(objUserLogin);
                        }
                    }
                    BindUsers(txtUserName.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                log.Error("gvLockedUsers_RowCommand \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ gvLockedUsers_RowCommand ", ex.Message + " \n " + ex.StackTrace);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLockedUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView grdLockedUser = (GridView)sender;
                GridViewRow grdCurrentRow = e.Row;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string strPhysicianID = grdLockedUser.DataKeys[grdCurrentRow.RowIndex].Value.ToString();
                    ImageButton imgLock = (ImageButton)e.Row.FindControl("lnkLock");
                    imgLock.CommandArgument = DataBinder.Eval(e.Row.DataItem, "UserLogin.AccountLocked").ToString();

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "UserLogin.AccountLocked")))
                    {
                        imgLock.ImageUrl = "~/Style/Images/Lock.png";
                        imgLock.ToolTip = "UnLock User";
                        imgLock.Height = 24;
                        imgLock.Width = 19;

                    }
                    else
                    {
                        imgLock.ImageUrl = "~/Style/Images/Unlock.png";
                        imgLock.ToolTip = "Lock User";
                        imgLock.Height = 24;
                        imgLock.Width = 26;

                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("gvLockedUsers_RowDataBound \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                ExceptionLog.WriteLog(PageName + " @ gvLockedUsers_RowDataBound ", ex.Message + " \n " + ex.StackTrace);
            }
        }
    }
}