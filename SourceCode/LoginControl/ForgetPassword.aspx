<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="LoginControl.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
 <script type="text/javascript">

     function RedirectHome() {
         location.href = "Login.aspx";
         return false;
     }

    </script>
    <h2 class="page-title">
        Forgot Password</h2>
    <div class="user-profile wrapper">
        <asp:Label runat="server" ID="lblErrorMsg" CssClass="redcolor"></asp:Label>
        <div class="panel-group block-seperator">
            <p>
                <label for="userName">
                    Email Address<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" ID="txtEmail" MaxLength="50"  CssClass="inputfield txtmarginleft"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Email required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"
                    CssClass="redcolor" ErrorMessage="Please enter valid email address." ValidationGroup="UserValidation" />
                <asp:Button runat="server" ID="btnSendMail" Width="15%" Text="Send Mail" ValidationGroup="UserValidation"
                    OnClick="btnSendMail_Click" />
                <asp:Button runat="server" ID="btnHome" Width="10%" Text="Home" OnClientClick="return RedirectHome();" />
            </p>
        </div>
    </div>
</asp:Content>
