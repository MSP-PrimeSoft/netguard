<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LoginControl.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Active Directory Integration::Login</title>
    <link href="Style/Login.css" rel='stylesheet' type='text/css' />
    <script type="text/javascript">
        function ToggleControls() {

            var txtUsername = document.getElementById("<%=txtUserName.ClientID %>");
            var txtPassword = document.getElementById("<%=txtPassword.ClientID %>");
            var lblUsernamReq = document.getElementById("<%=lblUsernamReq.ClientID %>");
            var lblPasswordReq = document.getElementById("<%=lblPasswordReq.ClientID %>");
            var lblError = document.getElementById("<%=lblError.ClientID %>");

            var RB1 = document.getElementById("<%=rdolstAuthenticationType.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    if (radio[i].value == "0") {
                        txtUsername.value = "";
                        txtPassword.value = "";
                        lblError.style.display = "none";
                        txtUsername.style.display = "none";
                        txtPassword.style.display = "none";
                        divRememberme.style.display = "none";
                        lblUsernamReq.style.display = "none";
                        lblPasswordReq.style.display = "none";
                    }
                    else {
                        lblError.style.display = "none";
                        txtUsername.style.display = "block";
                        txtPassword.style.display = "block";
                        divRememberme.style.display = "block";
                    }
                }
            }
        }

        function CheckValidations() {

            var txtUsername = document.getElementById("<%=txtUserName.ClientID %>");
            var txtPassword = document.getElementById("<%=txtPassword.ClientID %>");
            var lblUsernamReq = document.getElementById("<%=lblUsernamReq.ClientID %>");
            var lblPasswordReq = document.getElementById("<%=lblPasswordReq.ClientID %>");
            var lblError = document.getElementById("<%=lblError.ClientID %>");

            lblError.style.display = "none";

            var RB1 = document.getElementById("<%=rdolstAuthenticationType.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    if (radio[i].value == "1") {
                        if (txtUsername.value == '' && txtPassword.value == '') {

                            lblUsernamReq.style.display = "block";
                            lblPasswordReq.style.display = "block";
                            return false;

                        }
                        else if (txtUsername.value == '') {
                            lblUsernamReq.style.display = "block";
                            lblPasswordReq.style.display = "none";
                            return false;
                        }
                        else if (txtPassword.value == '') {
                            lblUsernamReq.style.display = "none";
                            lblPasswordReq.style.display = "block";
                            return false;
                        }
                        else {
                            lblUsernamReq.style.display = "none";
                            lblPasswordReq.style.display = "none";
                            return true;
                        }
                    }

                }
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="login-form">
            <h1 class="LoginHeading">
                Login</h1>
            <div class="formCss">
                <asp:Label ID="lblError" runat="server" CssClass="redcolor"></asp:Label>
                <asp:TextBox runat="server" ID="txtUserName" autocomplete="off" placeholder="Username"
                    Style="margin-bottom: 0px;"></asp:TextBox>
                <asp:Label ID="lblUsernamReq" Text="Username Reqired." runat="server" CssClass="redcolor"
                    Style="display: none;"></asp:Label><div style="height: 15px;">
                    </div>
                <asp:TextBox runat="server" ID="txtPassword" autocomplete="off" placeholder="Password"
                    Style="margin-bottom: 0px;" TextMode="Password"></asp:TextBox>
                <asp:Label ID="lblPasswordReq" Text="Password Reqired." runat="server" Style="display: none;"
                    CssClass="redcolor"></asp:Label><div style="height: 15px;">
                    </div>
                <asp:RadioButtonList runat="server" ID="rdolstAuthenticationType" onchange="ToggleControls();"
                    RepeatDirection="Horizontal" CssClass="labelClass">
                    <asp:ListItem Text="Active Directory" Value="0"></asp:ListItem>
                    <asp:ListItem Text="SQL Server" Value="1" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
                <div style="padding-top: 10px;" id="divRememberme">
                    <input type="checkbox" runat="server" id="chkRememberPassword" /><span class="labelClass"
                        style="padding-left: 0px !important;">Remember me!</span>
                </div>
                <div class="submit">
                    <asp:Button runat="server" ID="btnLogin" Text="Login" OnClientClick="return CheckValidations();"
                        OnClick="btnLogin_Click" />
                </div>
                <p class="labelClass">
                    <a href="ForgetPassword.aspx">Forgot Password ?</a></p>
                <p class="labelClass">
                    Don't have an account yet ?&nbsp;<a href="Registration.aspx">Sign Up</a></p>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
