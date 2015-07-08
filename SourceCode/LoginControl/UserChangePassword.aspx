<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserChangePassword.aspx.cs" Inherits="LoginControl.UserChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<h2 class="page-title">
        Change Password</h2>
    <div class="user-profile wrapper">
        <asp:Label ID="lblError" runat="server" CssClass="redcolor" Visible="false"></asp:Label>
        <div class="panel-group block-seperator">
            <p>
                <label for="userName">
                    Password<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" ID="txtPassword" autocomplete="off" TextMode="Password"
                    CssClass="inputfield txtmarginleft" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    New Password<span class="red-star" >*</span></label><asp:TextBox runat="server" ID="txtNewPassword" autocomplete="off"
                        CssClass="inputfield" TextMode="Password" MaxLength="50"></asp:TextBox>
                <asp:CustomValidator ID="cvNewPassword" ErrorMessage="" OnServerValidate="ValidatePassword"
                    ValidationGroup="UserValidation" runat="server" ControlToValidate="txtPassword"
                    CssClass="redcolor" />
                <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                    ErrorMessage="New Password required" CssClass="redcolor" Display="Dynamic" ValidationGroup="UserValidation"></asp:RequiredFieldValidator></p>
            <p>
                <label for="userName">
                    Confirm Password<span class="red-star" >*</span></label>
                <asp:TextBox runat="server" ID="txtNewConfirmPassword" autocomplete="off" TextMode="Password"
                    CssClass="inputfield txtmarginleft" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNewConfirmPassword" runat="server" ControlToValidate="txtNewConfirmPassword"
                    ErrorMessage="Confirm Password required" CssClass="redcolor" Display="Dynamic"
                    ValidationGroup="UserValidation"></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="cmpNewConfirmPassword" ControlToValidate="txtNewConfirmPassword"
                    CssClass="redcolor" ControlToCompare="txtNewPassword" Operator="Equal" Type="String"
                    ErrorMessage="New Password and confirm password does not match." ValidationGroup="UserValidation" /></p>
            <p>
                <label for="userName">
                </label>
                <asp:Button runat="server" ID="btnChangePassword" Text="Submit" Width="100px" ValidationGroup="UserValidation"
                    OnClick="btnChangePassword_Click" />
            </p>
        </div>
    </div>
</asp:Content>
