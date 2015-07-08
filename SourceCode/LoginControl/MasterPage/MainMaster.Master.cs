using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginControl.Logs;
using LoginControl.BusinessLayer;
using log4net;
using LoginControl.Model;

namespace LoginControl.MasterPage
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {

        #region Global Varriables

        private int userId = 0;

        UserBLL objUserBLL = new UserBLL();
        CommonBLL objCommonBLL = new CommonBLL();
        ILog log = LogManager.GetLogger("Logout");
        const string COMMONDATA = "MasterPage_MasterPage";
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {

                    if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])) || !string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
                        {
                            lblUserName.Text = Convert.ToString(Session["UserName"]);

                            MenuItemCollection menuItems = mTopMenu.Items;
                            MenuItem adminItem = new MenuItem();
                            foreach (MenuItem menuItem in menuItems)
                            {
                                if (menuItem.Text == "Change Password")
                                    adminItem = menuItem;
                            }
                            menuItems.Remove(adminItem);
                        }
                        else if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                        {
                            userId = Convert.ToInt32(Session["UserId"]);
                            User objUser = objUserBLL.GetUserDetailsById(userId);
                            if (objUser.Id != 0)
                            {
                                lblUserName.Text = objUser.FirstName + " " + objUser.LastName;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(Session["RoleId"])))
                            {
                                if (Convert.ToInt32(Session["RoleId"]) != 1)
                                {
                                    MenuItemCollection menuItems = mTopMenu.Items;
                                    MenuItem LockedUserItem = new MenuItem();
                                    MenuItem LoginHistoryItem = new MenuItem();
                                    foreach (MenuItem menuItem in menuItems)
                                    {
                                        if (menuItem.Text == "Locked Users")
                                            LockedUserItem = menuItem;

                                        if (menuItem.Text == "Login History")
                                            LoginHistoryItem = menuItem;
                                    }
                                    menuItems.Remove(LockedUserItem);
                                    menuItems.Remove(LoginHistoryItem);
                                }
                            }
                        }
                        foreach (MenuItem item in mTopMenu.Items)
                        {
                            if (Request.Url.AbsoluteUri.ToLower().Contains(Page.ResolveUrl(item.NavigateUrl).ToLower()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("Logout.aspx", true);
                    }
                }

                catch (Exception ex)
                {
                    log.Error("Page_Load \n Message: " + ex.Message + "\n Source: " + ex.Source + "\n StackTrace: " + ex.StackTrace);
                    ExceptionLog.WriteLog(COMMONDATA + " @ Page_Load ", ex.Message + " \n " + ex.StackTrace);
                }
            }
        }
        
    }
}